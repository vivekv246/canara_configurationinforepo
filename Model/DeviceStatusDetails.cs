using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{[Keyless]
    public class DeviceStatusDetails
    {
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceStatusID { get; set; }
     
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceTypeID { get; set; }
        public string DeviceType { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal DeviceID { get; set; }
        public string? DeviceName{ get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal StatusTypeId { get; set; }
        public string? StatusType{ get; set; }
        public string? DeviceStatus{ get; set; }
        public string? DeviceStatusSeverity { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? No_Recurrences { get; set; }
        public string? DeviceStatusDescription{ get; set; }
        public string? CreateTT { get; set; }
        public string? CloseTT { get; set; }
        public string? IgnoreTT { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? CreatedBy { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? ModifiedBy { get; set; }
}
}
