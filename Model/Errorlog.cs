using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    public class Errorlog
    {
        public int KioskID { get; set; }
        public string Method { get; set; }
        public string Location { get; set; }
        public string ErrorDetail { get; set; }
    }
}
