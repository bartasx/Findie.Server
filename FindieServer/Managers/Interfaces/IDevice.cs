using System.Threading.Tasks;
using FindieServer.Enums;

namespace FindieServer.Managers.Interfaces
{
    public interface IDevice
    {
        Task<TypeOfDevice> GetDeviceType();
        Task<DeviceState> GetDeviceState();
        Task<TypeOfDevice> SetDeviceType();       
    }
}