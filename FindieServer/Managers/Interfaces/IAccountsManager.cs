using System.Threading.Tasks;
using Findie.Common.Models;

namespace FindieServer.Managers.Interfaces
{
    public interface IAccountsManager
    {
        Task<string> LoginUser(LoginModel model);
        Task<string> RegisterAccount(RegisterModel model);
        void Logout();
        Task<bool> ChangeCredentials(UserInfo userInfo);
    }
}
