using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class DeviceDetails
    {
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceID { get; set; }
        public string Device { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceTypeID { get; set; }
        public string DeviceType { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal CreatedBy { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal ModifiedBy { get; set; }
    }
}
