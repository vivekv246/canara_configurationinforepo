using ConfigurationInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Repository.IRepository
{
    public interface IDeviceStatusRepository
    {
        Task<List<DeviceStatusDetails>> GetDeviceStatusDetails();
        Task<Response> InsertDeviceStatus(DeviceStatusDetails InsertDeviceStatus);
        Task<Response> UpdateDeviceStatus(DeviceStatusDetails UpdateDeviceStatus);
        Task<Response> DeleteDeviceStatus(DeviceStatusDetails DeleteDeviceStatus);
        Task<List<DeviceStatusModel>> GetDeviceName(DeviceStatusModel GetDeviceName);
        Task<List<Statustype>> GetStatustypes();
    }
}
