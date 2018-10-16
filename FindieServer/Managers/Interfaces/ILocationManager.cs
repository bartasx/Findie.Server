using System.Collections.Generic;
using System.Threading.Tasks;
using Findie.Common.Models;

namespace FindieServer.Managers.Interfaces
{
    public interface ILocationManager
    {
        Task<LocationModel> GetSpecificUserLocation(string username);
        Task SendLocation(LocationModel model);
        Task<List<LocationModel>> GetFriendsLocation(string username);
    }
}