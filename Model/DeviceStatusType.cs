using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class DeviceStatusType
    {
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceTypeID { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceID { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal StatusTypeId{get;set;}
        public string StatusType { get; set; }

    }
}
