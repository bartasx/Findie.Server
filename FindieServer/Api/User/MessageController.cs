using System.Collections.Generic;
using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace FindieServer.Api.User
{
    [Produces("application/json")]
    [Route("api/User/Message")]
    public class MessageController : Controller
    {
        private readonly IMessageManager _message;

        public MessageController(IMessageManager message)
        {
            this._message = message;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<List<Messages>> GetMessages(string username, string secondUsername)
        {
           return await this._message.GetMessages(username, secondUsername);
        }
    }
}