using System;
using Microsoft.EntityFrameworkCore;

namespace configurationinfo.Model
{

    [Keyless]
    public class ProblemConfigration
    {

        public Int32? ProblemID { get; set; }

        public string Problem { get; set; }

        public string ProblemCode { get; set; }

        public string ProblemDecsription { get; set; }

        public string ProblemSeverity { get; set; }

        public string ProblemCategory { get; set; }

        public Int32? ProblemCategoryID { get; set; }

    
        public string DeviceType { get; set; }

        public Int32? DeviceID { get; set; }

        public string DeviceName { get; set; }

        public Int32? DeviceTypeID { get; set; }
        

        public string StatusType { get; set; }

        public Int32? DeviceStatusTypeID { get; set; }


        public string DeviceStatus { get; set; }

        public Int32? DeviceStatusID { get; set; }
        
        public string ProblemResolution { get; set; }

        public string ProblemColorCode { get; set; }

        public string ProblemIcon { get; set; }

        public Boolean CalculateDownTime { get; set; }

        
        public Int32? InsertedBy { get; set; }

        public Int32? ModifiedBy { get; set; }



    }
}









































