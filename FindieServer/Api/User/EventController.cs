using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindieServer.Api.User
{
    [Produces("application/json")]
    [Route("Api/User/Event")]
    public class EventController : Controller
    {
        private readonly IEventsManager _eventsManager;

        public EventController(IEventsManager eventsManager)
        {
            this._eventsManager = eventsManager;
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetFile(EventModel eventModel)
        {
            var image = await this._eventsManager.GetEventImage(eventModel);

            if (image != null)
            {
                return new FileStreamResult(image, "image/jpeg");
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        [Route("GetSubscribedEvents")]
        public async Task<IActionResult> GetSubscribedEvents(string username)
        {
            var events = await this._eventsManager.GetAllSubscribedEvents(username);

            if (events != null)
            {
                return Ok(events);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        [Route("GetComments")]
        public async Task<IActionResult> GetComments(int eventId)
        {
            var comments = await this._eventsManager.GetComments(eventId);
            if (comments != null)
            {
                return Ok(comments);
            }

            return Ok();
        }

        [HttpPost]
        [Route("UploadNewEvent")]
        public async Task<IActionResult> CreateNewEventAsync([FromForm] EventModel model)
        {
            if (model != null)
            {
                await this._eventsManager.CreateNewEventAsync(model);
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        [Route("AddNewComment")]
        public async Task<IActionResult> AddNewCommentAsync([FromBody] EventCommentModel model)
        {
            await this._eventsManager.CommentSpecificEvent(model);

            return Ok();
        }

        [HttpPut]
        [Route("EditEvent")]
        public async Task<IActionResult> EditEventContent([FromForm] EventModel model)
        {
            await this._eventsManager.EditEvent(model);
            return Ok();
        }

        [HttpPut]
        [Route("AcceptEventRequest")]
        public async Task<IActionResult> AcceptEventRequest([FromBody] InvitationModel model)
        {
            await this._eventsManager.AcceptEventRequest(model);
            return Ok();
        }

        [HttpPut]
        [Route("InviteUserToEvent")]
        public async Task<IActionResult> InviteUserToEvent([FromBody] InvitationModel model)
        {
            await this._eventsManager.InviteUserToEvent(model);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(EventModel model)
        {
            var result = await Task.FromResult(this._eventsManager.DeleteEvent(model));

            if (result.IsCompletedSuccessfully)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        [Route("RejectEventRequest")]
        public async Task<IActionResult> RejectEventRequest([FromBody] InvitationModel model)
        {
            await this._eventsManager.RejectEventRequest(model);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCommentEvent")]
        public async Task<IActionResult> DeleteCommentEvent()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("KickUserFromEvent")]
        public async Task<IActionResult> KickUserFromEvent([FromBody] InvitationModel model)
        {
            var result = await Task.FromResult(this._eventsManager.KickUserFromEvent(model));

            if (result.IsCompletedSuccessfully)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}