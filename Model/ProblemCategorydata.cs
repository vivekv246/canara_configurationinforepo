using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class ProblemCategorydata
    {
        public string ProblemCategory { get; set; }

        public Decimal? ProblemCategoryID { get; set; }
    }
}
