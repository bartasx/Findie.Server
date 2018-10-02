using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FindieServer.Models;

namespace FindieServer.Managers.Interfaces
{
    public interface IEventsManager
    {
        Task CreateNewEventAsync(EventModel model);
        Task EditEvent(EventModel model);
        Task<MemoryStream> GetEventImage(EventModel model);
        Task DeleteEvent(EventModel eventModel);
        Task InviteUserToEvent(InvitationModel model);
        Task AcceptEventRequest(InvitationModel model);
        Task RejectEventRequest(InvitationModel model);
        Task KickUserFromEvent(InvitationModel model);
        Task<List<EventCommentModel>> GetComments(int id);
        Task<List<EventModel>> GetAllSubscribedEvents(string username);
        Task CommentSpecificEvent(EventCommentModel eventCommentModel);
        Task RemoveCommentFromSpecificEvent();
    }
}