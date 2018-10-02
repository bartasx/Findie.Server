using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models.DbModels;
using Microsoft.AspNetCore.SignalR;

namespace FindieServer
{
    public class ApplicationHub : Hub
    {
        #region Fields
        public static readonly ConcurrentDictionary<string, string> OnlineUsersDictionary =
            new ConcurrentDictionary<string, string>();

        private readonly DatabaseContext _dbContext;
        private readonly IMessageManager _messageManager;

        public ApplicationHub(DatabaseContext context, IMessageManager messageManager)
        {
            this._dbContext = context;
            this._messageManager = messageManager;
        }
        #endregion

        public void Connect(string username)
        {
            var connectionId = Context.ConnectionId;

            OnlineUsersDictionary.TryAdd(username, connectionId);
            this.ChangeUserStatus(username, false);
        }

        public async void Send(string fromUsername, string toUsername, string message)
        {
            var clientConnectionId = OnlineUsersDictionary.FirstOrDefault(x => x.Key == toUsername);

            if (clientConnectionId.Value != null)
            {
                await this.Clients.Client(clientConnectionId.Value).SendAsync("broadcast", fromUsername, toUsername, message);
            }

            this._messageManager.SendMessage(fromUsername, toUsername, message);
        }

        public override async Task OnConnectedAsync()
        {
            // return await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Task.Run(() =>
            {
                var userConnectionId = OnlineUsersDictionary.FirstOrDefault(user => user.Value == Context.ConnectionId);

                ((IDictionary)OnlineUsersDictionary).Remove(userConnectionId);

                this.ChangeUserStatus(userConnectionId.Key, true);
            });
        }

        public void Location(double latitude, double longitude, string username)
        {
            this.Clients.All.SendAsync("sendLocation", latitude, longitude, username);
        }

        #region Private methods
        private void ChangeUserStatus(string username, bool isUserOnline)
        {
            var query = this._dbContext.Users.FirstOrDefault(x => x.UserName == username);

            if (query != null)
            {
                this._dbContext.Users.Attach(query);
                if (!isUserOnline)
                {
                    query.IsUserOnline = true;
                }
                else
                {
                    query.IsUserOnline = false;
                }
                this._dbContext.SaveChangesAsync();
            }
        }
        #endregion
    }
}