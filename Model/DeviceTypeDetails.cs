using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class DeviceTypeDetails
    {
        [Column(TypeName = "decimal(18,0)")]
        public decimal? DeviceTypeID { get; set; }
        public string? DeviceType { get; set; }
    }
}

