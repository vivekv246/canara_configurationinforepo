using configurationinfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    public class ConfigInfo
    {
        public IEnumerable<DeviceDetails> DeviceDetails { get; set; }

        public IEnumerable<DeviceStatusDetails> DeviceStatus { get; set; }

        public IEnumerable<ProblemConfigration> ProblemConfig { get; set; }
        public IEnumerable<DeviceDetailsMaster> Devicedetail { get; set; }

        public IEnumerable<ProblemCategorydata> GetCategory { get; set; }
        public IEnumerable<Statustype> GetStatustypes { get; set; }
        public IEnumerable<DeviceTypeDetails> GetDevicetype { get; set; }


    }
}
