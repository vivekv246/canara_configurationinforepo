using ConfigurationInfo.DatabaseContext;
using ConfigurationInfo.Model;
using ConfigurationInfo.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Repository
{
    public class LogRepository: ILogRepository
    {
        private readonly ApplicationDBContext _appContext;
        public LogRepository(ApplicationDBContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<Response> LogInsertion(LogMetaData logMetaData)
        {
            Response response = new Response();

            List<SqlParameter> parms = new List<SqlParameter>
            {

                 new SqlParameter { ParameterName = "@TerminalId ", Value =  logMetaData.TerminalId},
                 new SqlParameter { ParameterName = "@Url", Value  = logMetaData.Url } ,
                 new SqlParameter { ParameterName = "@GeteWayUrl", Value   =  logMetaData.GeteWayUrl}  ,
                 new SqlParameter { ParameterName = "@microServiceName ", Value   =  logMetaData.microServiceName}  ,
                 new SqlParameter { ParameterName = "@MethodName", Value  =  logMetaData.MethodName}  ,
                 new SqlParameter { ParameterName = "@Request", Value =  logMetaData.Request}  ,
                 new SqlParameter { ParameterName = "@Response", Value  =  logMetaData.Response} ,
                 new SqlParameter { ParameterName = "@RequestTime", Value  =  logMetaData.RequestTime} ,
                 new SqlParameter { ParameterName = "@ResponseTime", Value  =  logMetaData.ResponseTime} ,
                 new SqlParameter { ParameterName = "@ResponseHttpCode", Value   =  logMetaData.ResponseHttpCode}  ,
                 new SqlParameter { ParameterName = "@serverName", Value   =  logMetaData.ServerName}  ,
                 new SqlParameter { ParameterName = "@requestHeaders", Value   =  logMetaData.RequestHeaders} ,
                 new SqlParameter { ParameterName = "@responseHeaders", Value   =  logMetaData .ResponseHeaders}
                };





            var result = await _appContext.InsertLogs.FromSqlRaw($"exec spd_insertLogs @TerminalId , @Url, @GeteWayUrl, @microServiceName , @MethodName , @Request , @Response, @RequestTime, @ResponseTime , @ResponseHttpCode , @serverName , @requestHeaders , @responseHeaders", parms.ToArray()).ToListAsync();

            return response;
        }
    }
}
