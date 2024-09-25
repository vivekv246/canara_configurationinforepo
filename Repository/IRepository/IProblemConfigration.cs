using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using configurationinfo.Model;
using ConfigurationInfo.Model;

namespace configurationinfo.Repository.IRepository
{
    public interface IProblemConfigration
    {
         
        public Task<List<ProblemConfigration>> GetProblemConfigrationInformation(); 


         public Task<Response> InsertProblemConfigration(ProblemConfigration problemConfigration);

        public Task<Response> UpdateProblemConfigration(ProblemConfigration problemConfigration);


        public Task<Response> DeleteProblemConfigration(Int32? ProblemId);
        public Task<List<DeviceStatusType>> GetDeviceStatusType(DeviceStatusType GetDeviceStatusType);
        public Task<List<DeviceStatusData>> GetDeviceStatus(DeviceStatusData GetDeviceStatus);
        public Task<List<DeviceDetailsMaster>> GetDeviceMaster();
        public Task<List<ProblemCategorydata>> GetCategory();

    }
}