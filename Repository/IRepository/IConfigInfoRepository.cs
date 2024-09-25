using configurationinfo.Model;
using ConfigurationInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Repository.IRepository
{
    public interface IConfigInfoRepository
    {
        public Task<ConfigInfo> GetDeviceStatus();
        public Task<ConfigInfo> ProblemConfig();
       
    
    }
}
