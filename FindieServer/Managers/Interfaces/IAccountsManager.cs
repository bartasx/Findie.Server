using System.Threading.Tasks;
using FindieServer.Models;
using FindieServer.ViewModels;

namespace FindieServer.Managers.Interfaces
{
    public interface IAccountsManager
    {
        Task<string> LoginUser(LoginViewModel model);
        Task<string> RegisterAccount(RegisterViewModel model);
        void Logout();
        Task<bool> ChangeCredentials(UserInfo userInfo);
    }
}
