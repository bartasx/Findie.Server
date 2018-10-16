using System.Collections.Generic;
using System.Threading.Tasks;
using FindieServer.DbModels;

namespace FindieServer.Managers.Interfaces
{
    public interface IMessageManager
    {
        Task<List<Messages>> GetMessages(string username, string secondUsername);
        void SendMessage(string fromUsername, string toUsername, string message);
        Task<Messages> DeleteMessage(Messages message);
    }
}