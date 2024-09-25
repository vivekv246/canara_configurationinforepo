using ConfigurationInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Repository.IRepository
{
    public interface IDeviceDetailsRepository
    {
        Task<List<DeviceDetails>> GetDeviceDetails();
        Task<Response> InsertDeviceDetails(DeviceDetails InsertDeviceDetails);
        Task<Response> UpdateDeviceDetails(DeviceDetails UpdateDeviceDetails);

        Task<Response> DeleteDeviceDetails(DeviceDetails DeleteDeviceDetails);
        Task<List<DeviceTypeDetails>> GetDeviceType();
    }
}
