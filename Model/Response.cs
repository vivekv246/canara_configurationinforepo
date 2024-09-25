using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class Response
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
