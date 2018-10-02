using System.Collections.Generic;
using System.Threading.Tasks;
using FindieServer.Models;

namespace FindieServer.Managers.Interfaces
{
    public interface IFriendsManager
    {
        Task<List<string>> GetFriendsList(string username);
        Task<UserInfo> GetSpecificUserInfo(string username);
        Task<List<string>> GetUserByNameFromSearchbar(string username);

        Task<bool> RemoveFriendFromFriendsList(string senderUsername, string friendUsername);
        Task<bool> SendFriendRequest(string senderUsername, string friendUsername);
        Task<bool> AcceptFriendRequest(string username, string friendUsername);
    }
}