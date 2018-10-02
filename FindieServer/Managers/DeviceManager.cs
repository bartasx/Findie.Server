using System.Threading.Tasks;
using FindieServer.Enums;
using FindieServer.Managers.Interfaces;
using FindieServer.Models.DbModels;

namespace FindieServer.Managers
{
    public class DeviceManager : IDevice
    {
        private readonly DatabaseContext _dbContext;

        public DeviceManager(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        public Task<DeviceState> GetDeviceState()
        {
            throw new System.NotImplementedException();
        }

        public Task<TypeOfDevice> GetDeviceType()
        {
            throw new System.NotImplementedException();
        }

        public Task<TypeOfDevice> SetDeviceType()
        {
            throw new System.NotImplementedException();
        }       
    }
}