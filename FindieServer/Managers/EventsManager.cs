using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models;
using FindieServer.Models.DbModels;
using FindieServer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Managers
{
    public class EventsManager : IEventsManager
    {
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public EventsManager(DatabaseContext dbContext, UserManager<AppUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public async Task CreateNewEventAsync(EventModel model)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var uploads = Path.Combine(homePath, "uploads/events");

            var newEvent = new Events()
            {
                HostId = this._userManager.FindByNameAsync(model.HostUsername).Result.Id,
                DateOfCreate = DateTime.Now,
                EventDate = DateTime.Now,
                EventDescription = model.EventDescription,
                IsEventPrivate = false,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                EventName = model.EventName
            };

            if (model.File != null)
            {
                newEvent.ImageUri = $"{uploads}/{await this.GenerateImageName()}";

                using (var fileStream =
                    new FileStream(newEvent.ImageUri, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
            }

            try
            {
                await this._dbContext.Events.AddAsync(newEvent);
                await this._dbContext.SaveChangesAsync();

                var newCreatedEvent =
                    await (from events in this._dbContext.Events where events == newEvent select events)
                        .FirstOrDefaultAsync();

                var eventParticipant = new EventParticipants()
                {
                    UserId = newEvent.HostId,
                    IsUserAcceptedEventRequest = true,
                    EventId = newCreatedEvent.Id
                };

                this._dbContext.EventParticipants.Add(eventParticipant);
                await this._dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task EditEvent(EventModel model)
        {
            var query = await (from events in this._dbContext.Events where events.Id == model.EventId select events)
                .FirstOrDefaultAsync();
            if (query != null)
            {
                query.Latitude = model.Latitude;
                query.Longitude = model.Longitude;
                query.EventDate = model.DateOfEvent;
                query.EventDescription = model.EventDescription;
                query.EventName = model.EventName;

                if (model.File != null && query.ImageUri != null)
                {
                    File.Delete(query.ImageUri);

                    string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                                       Environment.OSVersion.Platform == PlatformID.MacOSX)
                        ? Environment.GetEnvironmentVariable("HOME")
                        : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

                    var uploads = Path.Combine(homePath, "uploads/events");

                    query.ImageUri = $"{uploads}/{await this.GenerateImageName()}";

                    using (var fileStream =
                        new FileStream(query.ImageUri, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                        await this._dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task DeleteEvent(EventModel model)
        {
            var query = await (from userEvent in this._dbContext.Events
                where userEvent.EventDescription == model.EventDescription && userEvent.EventDate == model.DateOfEvent
                select userEvent).FirstOrDefaultAsync();

            if (query != null)
            {
                this._dbContext.Events.Remove(query);
                await this._dbContext.SaveChangesAsync();
            }
        }

        public async Task InviteUserToEvent(InvitationModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var eventParticipant = new EventParticipants()
                {
                    EventId = model.EventId,
                    UserId = user.Id,
                    IsUserAcceptedEventRequest = false
                };
                try
                {
                    this._dbContext.EventParticipants.Add(eventParticipant);
                    await this._dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task AcceptEventRequest(InvitationModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);
            var query = await (from p in this._dbContext.EventParticipants
                where model.EventId == p.EventId && p.UserId == user.Id
                select p).FirstOrDefaultAsync();

            if (query != null)
            {
                var eventRequest =
                    await (from p in this._dbContext.EventParticipants
                        where p.EventId == query.EventId && p.UserId == query.UserId
                        select p).FirstOrDefaultAsync();

                if (eventRequest != null)
                {
                    eventRequest.IsUserAcceptedEventRequest = true;
                    try
                    {
                        await this._dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task RejectEventRequest(InvitationModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);
            var query = await (from p in this._dbContext.EventParticipants
                where model.EventId == p.EventId && p.UserId == user.Id
                select p).FirstOrDefaultAsync();

            if (query != null)
            {
                var eventRequest =
                    await (from p in this._dbContext.EventParticipants
                        where p.EventId == query.EventId && p.UserId == query.UserId
                        select p).FirstOrDefaultAsync();

                if (eventRequest != null)
                {
                    try
                    {
                        this._dbContext.EventParticipants.Remove(eventRequest);
                        await this._dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task KickUserFromEvent(InvitationModel model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var query = await (from participants in this._dbContext.EventParticipants
                    where participants.EventId == model.EventId &&
                          participants.UserId == user.Id
                    select participants).FirstOrDefaultAsync();

                if (query != null)
                {
                    this._dbContext.Remove(query);
                    await this._dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<EventCommentModel>> GetComments(int id)
        {
            var query = await (from comments in this._dbContext.EventComments
                where comments.EventId == id
                select comments).ToListAsync();

            if (query != null)
            {
                var commentsList = new List<EventCommentModel>();

                foreach (var comment in query)
                {
                    commentsList.Add(new EventCommentModel()
                    {
                        Username = this._userManager.FindByIdAsync(comment.UserId.ToString()).Result.UserName,
                        UserId = comment.UserId,
                        TimeStamp = comment.TimeStamp,
                        Comment = comment.Comment,
                        EventId = comment.EventId
                    });
                }

                return commentsList;
            }

            return null;
        }

        public async Task<List<EventModel>> GetAllSubscribedEvents(string username)
        {
            var user = await this._userManager.FindByNameAsync(username);

            var userId = user.Id;

            var query = await (from events in this._dbContext.Events
                from participants in this._dbContext.EventParticipants
                where events.Id == participants.EventId && participants.UserId == userId
                select events).ToListAsync();

            var eventList = new List<EventModel>();

            foreach (var foundEvent in query)
            {
                var eventOwner = await this._userManager.FindByIdAsync(foundEvent.HostId.ToString());

                if (eventOwner != null)
                {
                    eventList.Add(new EventModel()
                    {
                        HostUsername = eventOwner.UserName,
                        Latitude = foundEvent.Latitude,
                        Longitude = foundEvent.Longitude,
                        EventDescription = foundEvent.EventDescription,
                        EventName = foundEvent.EventName,
                        DateOfEvent = DateTime.Now,
                        EventId = foundEvent.Id,
                    });
                }
            }

            return eventList;
        }

        public async Task<MemoryStream> GetEventImage(EventModel model)
        {
            var query = await (from events in this._dbContext.Events where events.Id == model.EventId select events)
                .FirstOrDefaultAsync();

            if (query != null)
            {
                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(query.ImageUri, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }

                memoryStream.Position = 0;
                return memoryStream;
            }

            return null;
        }

        public async Task CommentSpecificEvent(EventCommentModel model)
        {
            var query = await (from events in this._dbContext.Events where events.Id == model.EventId select events)
                .FirstOrDefaultAsync();

            var userInfo = await this._userManager.FindByNameAsync(model.Username);

            var newComment = new EventComments()
            {
                Comment = model.Comment,
                TimeStamp = DateTime.Now,
                EventId = query.Id,
                UserId = userInfo.Id
            };

            try
            {
                this._dbContext.EventComments.Add(newComment);
                await this._dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public Task RemoveCommentFromSpecificEvent()
        {
            throw new NotImplementedException();
        }

        private async Task<Guid> GenerateImageName()
        {
            var guid = Guid.NewGuid();
            var query = await (from events in this._dbContext.Events
                where events.ImageUri == guid.ToString()
                select events).FirstOrDefaultAsync();

            if (query != null)
            {
                await GenerateImageName();
            }

            return guid;
        }
    }
}