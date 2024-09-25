using ConfigurationInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Repository.IRepository
{
    public interface ILogRepository
    {
        public Task<Response> LogInsertion(LogMetaData logMetaData);
    }
}
