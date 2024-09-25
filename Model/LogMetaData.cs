using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Model
{
    [Keyless]
    public class LogMetaData
    {
        public string TerminalId { get; set; }
        public string Url { get; set; }
        public string GeteWayUrl { get; set; }
        public string microServiceName { get; set; }
        public string MethodName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string ResponseHttpCode { get; set; }
        public string ServerName { get; set; }
        public string RequestHeaders { get; set; }
        public string ResponseHeaders { get; set; }


    }
}
