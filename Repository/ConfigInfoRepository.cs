using MasterAPI.Repository.IRepository;
using System.Threading.Tasks;
using System;
using ConfigurationInfo.Repository.IRepository;
using ConfigurationInfo.Model;
using configurationinfo.Repository.IRepository;

namespace ConfigurationInfo.Repository
{
    public class ConfigInfoRepository : IConfigInfoRepository
    {
        public readonly IDeviceDetailsRepository _idevicedetails;
        public readonly IDeviceStatusRepository _idevicestatus;

        public readonly IProblemConfigration _iproblem;
        ICommonRepository _cRepository;

        public ConfigInfoRepository(IDeviceDetailsRepository idevicedetails, IDeviceStatusRepository idevicestatus,
            ICommonRepository cRepository, IProblemConfigration iproblem)
        {
            _idevicedetails = idevicedetails;
            _idevicestatus = idevicestatus;
            _cRepository = cRepository;
            _iproblem = iproblem;
        }
        public async  Task<ConfigInfo> GetDeviceStatus()
        {
            var result = new ConfigInfo();
            try
            {

                result.DeviceStatus = await _idevicestatus.GetDeviceStatusDetails();
                result.DeviceDetails = await _idevicedetails.GetDeviceDetails();
                result.GetStatustypes = await _idevicestatus.GetStatustypes();
                result.GetDevicetype = await _idevicedetails.GetDeviceType();
            }
            catch (Exception ex)
            {
                _cRepository.InsertErrorLog("", "GetDeviceStatus()", ex.Message);
            }
            return result;
        }

        public async Task<ConfigInfo> ProblemConfig()
        {
            var result = new ConfigInfo();
            try
            {
            
                result.Devicedetail = await _iproblem.GetDeviceMaster();
                result.ProblemConfig = await _iproblem.GetProblemConfigrationInformation();
                result.GetCategory = await _iproblem.GetCategory();
                 result.GetStatustypes = await _idevicestatus.GetStatustypes();
                result.GetDevicetype = await _idevicedetails.GetDeviceType();
                result.DeviceDetails = await _idevicedetails.GetDeviceDetails();
                result.DeviceStatus = await _idevicestatus.GetDeviceStatusDetails();


            }
            catch (Exception ex)
            {
                _cRepository.InsertErrorLog("", "ProblemConfig()", ex.Message);
            }
            return result;
        }
    }
}
