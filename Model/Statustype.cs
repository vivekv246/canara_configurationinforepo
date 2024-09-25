using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class Statustype
    {
        public decimal StatusTypeID { get; set; }
        public string StatusType { get; set; }
        public string StatusTypeDescription { get; set; }
    }
}
