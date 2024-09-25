using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{

    [Keyless]
    public class TokenExpiry
    {
        public DateTime TokenExpireTime { get; set; }
    }
}
