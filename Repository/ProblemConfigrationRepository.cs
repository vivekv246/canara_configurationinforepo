using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using configurationinfo.Model;
using configurationinfo.Repository.IRepository;
using ConfigurationInfo.DatabaseContext;
using ConfigurationInfo.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace configurationinfo.Repository
{
    public class ProblemConfigrationRepository : IProblemConfigration
    {

        private readonly ApplicationDBContext _appContext;

        public ProblemConfigrationRepository(ApplicationDBContext appContext)
        {
            _appContext = appContext;
        }



        public async Task<List<ProblemConfigration>> GetProblemConfigrationInformation()
        {

            try
            {
                var result = await _appContext.GetProblemConfigInfos.FromSqlRaw($"exec SPD_GetProblemConfigrationInformation").ToListAsync();
               
                return result;

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "GetProblemConfigrationInformation";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }
            return null;
        }

        public async Task<Response> InsertProblemConfigration(ProblemConfigration problemConfigration)
        {
            Response response = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                 new SqlParameter { ParameterName = "@Problem", Value = problemConfigration.Problem == null ? DBNull.Value : problemConfigration.Problem},
                 new SqlParameter { ParameterName = "@ProblemCode", Value = problemConfigration.ProblemCode == null ? DBNull.Value : problemConfigration.ProblemCode}  ,
                 new SqlParameter { ParameterName = "@ProblemDecsription", Value = problemConfigration.ProblemDecsription == null ? DBNull.Value : problemConfigration.ProblemDecsription},
                 new SqlParameter { ParameterName = "@ProblemSeverity", Value = problemConfigration.ProblemSeverity == null ? DBNull.Value : problemConfigration.ProblemSeverity},
                 new SqlParameter { ParameterName = "@ProblemCategoryID", Value = problemConfigration.ProblemCategoryID == null ? DBNull.Value : problemConfigration.ProblemCategoryID},
                 new SqlParameter { ParameterName = "@DeviceID", Value = problemConfigration.DeviceID == null ? DBNull.Value : problemConfigration.DeviceID},
                 new SqlParameter { ParameterName = "@DeviceTypeID", Value = problemConfigration.DeviceTypeID == null ? DBNull.Value : problemConfigration.DeviceTypeID} ,
                 new SqlParameter { ParameterName = "@DeviceStatusTypeID", Value = problemConfigration.DeviceStatusTypeID == null ? DBNull.Value : problemConfigration.DeviceStatusTypeID} ,
                 new SqlParameter { ParameterName = "@DeviceStatusID", Value = problemConfigration.DeviceStatusID == null ? DBNull.Value : problemConfigration.DeviceStatusID} ,
                 new SqlParameter { ParameterName = "@ProblemResolution", Value = problemConfigration.ProblemResolution == null ? DBNull.Value : problemConfigration.ProblemResolution} ,
                 new SqlParameter { ParameterName = "@ProblemColorCode", Value = problemConfigration.ProblemColorCode == null ? DBNull.Value : problemConfigration.ProblemColorCode} ,
                 new SqlParameter { ParameterName = "@ProblemIcon", Value = problemConfigration.ProblemIcon == null ? DBNull.Value : problemConfigration.ProblemIcon},
                 new SqlParameter { ParameterName = "@CalculateDownTime", Value = problemConfigration.CalculateDownTime == null ? DBNull.Value : problemConfigration.CalculateDownTime},
                 new SqlParameter { ParameterName = "@insertedBy", Value = problemConfigration.InsertedBy == null ? DBNull.Value : problemConfigration.InsertedBy}
                };

              var result = await _appContext.Response.FromSqlRaw($"exec SPD_InsertProblemConfigrationInformation @Problem , @ProblemCode , @ProblemDecsription , @ProblemSeverity , @ProblemCategoryID , @DeviceID , @DeviceTypeID , @DeviceStatusTypeID , @DeviceStatusID , @ProblemResolution , @ProblemColorCode , @ProblemIcon , @CalculateDownTime , @insertedBy" , parms.ToArray()).ToListAsync();
               foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };  

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "InsertProblemConfigration";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }

            return response;
        }

        public async Task<Response> UpdateProblemConfigration(ProblemConfigration problemConfigration)
        {
             Response response = new Response();
            try
            {
                 List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                 new SqlParameter { ParameterName = "@ProblemID", Value = problemConfigration.ProblemID == null ? DBNull.Value : problemConfigration.ProblemID},
                 new SqlParameter { ParameterName = "@Problem", Value = problemConfigration.Problem == null ? DBNull.Value : problemConfigration.Problem},
                 new SqlParameter { ParameterName = "@ProblemCode", Value = problemConfigration.ProblemCode == null ? DBNull.Value : problemConfigration.ProblemCode} ,
                 new SqlParameter { ParameterName = "@ProblemDecsription", Value = problemConfigration.ProblemDecsription == null ? DBNull.Value : problemConfigration.ProblemDecsription},
                 new SqlParameter { ParameterName = "@ProblemSeverity", Value = problemConfigration.ProblemSeverity == null ? DBNull.Value : problemConfigration.ProblemSeverity},
                 new SqlParameter { ParameterName = "@ProblemCategoryID", Value = problemConfigration.ProblemCategoryID == null ? DBNull.Value : problemConfigration.ProblemCategoryID},
                 new SqlParameter { ParameterName = "@DeviceID", Value = problemConfigration.DeviceID == null ? DBNull.Value : problemConfigration.DeviceID},
                 new SqlParameter { ParameterName = "@DeviceTypeID", Value = problemConfigration.DeviceTypeID == null ? DBNull.Value : problemConfigration.DeviceTypeID} ,
                 new SqlParameter { ParameterName = "@DeviceStatusTypeID", Value = problemConfigration.DeviceStatusTypeID == null ? DBNull.Value : problemConfigration.DeviceStatusTypeID} ,
                 new SqlParameter { ParameterName = "@DeviceStatusID", Value = problemConfigration.DeviceStatusID == null ? DBNull.Value : problemConfigration.DeviceStatusID} ,
                 new SqlParameter { ParameterName = "@ProblemResolution", Value = problemConfigration.ProblemResolution == null ? DBNull.Value : problemConfigration.ProblemResolution} ,
                 new SqlParameter { ParameterName = "@ProblemColorCode", Value = problemConfigration.ProblemColorCode == null ? DBNull.Value : problemConfigration.ProblemColorCode} ,
                 new SqlParameter { ParameterName = "@ProblemIcon", Value = problemConfigration.ProblemIcon == null ? DBNull.Value : problemConfigration.ProblemIcon},
                 new SqlParameter { ParameterName = "@CalculateDownTime", Value = problemConfigration.CalculateDownTime == null ? DBNull.Value : problemConfigration.CalculateDownTime},
                 new SqlParameter { ParameterName = "@ModifiedBy", Value = problemConfigration.ModifiedBy == null ? DBNull.Value : problemConfigration.ModifiedBy}
                };

              var result = await _appContext.Response.FromSqlRaw($"exec SPD_UpdateProblemConfigrationInformation @ProblemID , @Problem , @ProblemCode , @ProblemDecsription , @ProblemSeverity , @ProblemCategoryID , @DeviceID , @DeviceTypeID , @DeviceStatusTypeID , @DeviceStatusID , @ProblemResolution , @ProblemColorCode , @ProblemIcon , @CalculateDownTime , @ModifiedBy" , parms.ToArray()).ToListAsync();
               foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };  

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "UpdateProblemConfigration";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }

            return response;
        }


        public async Task<Response> DeleteProblemConfigration(Int32? ProblemId)
        {
            Response response = new Response();
            try
            {
            List<SqlParameter> parms = new List<SqlParameter>       
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@ProblemID", Value = ProblemId}
            };

            var result = await _appContext.Response.FromSqlRaw(@"exec SPD_DeleteProblemConfigrationInformation @ProblemID", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "DeleteProblemConfigration";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }

            return response;
        }


        public Task<int> InsertErrorLog(Errorlog errorlog)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@KioskID", Value = errorlog.KioskID},
                new SqlParameter { ParameterName = "@Method", Value = errorlog.Method},
                new SqlParameter { ParameterName = "@ErrorDetail", Value = errorlog.ErrorDetail}
            };
            int result = _appContext.Database.ExecuteSqlRaw(@"exec SP_INS_ErrorLog @KioskID , @Method , @ErrorDetail", parms.ToArray());
            return Task.FromResult(result);
        }

        public async Task<List<DeviceStatusType>> GetDeviceStatusType(DeviceStatusType GetDeviceStatusType)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = GetDeviceStatusType.DeviceTypeID},
                 new SqlParameter { ParameterName = "@DeviceID", Value = GetDeviceStatusType.DeviceID},
            };
            var result = await _appContext.GetDeviceStatusType.FromSqlRaw($"exec spd_GetDeviceStatusType @DeviceTypeID,@DeviceID", parms.ToArray()).ToListAsync();
            return result;
        }
        public async Task<List<DeviceStatusData>> GetDeviceStatus(DeviceStatusData GetDeviceStatus)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = GetDeviceStatus.DeviceTypeID},
                 new SqlParameter { ParameterName = "@DeviceID", Value = GetDeviceStatus.DeviceID},
                  new SqlParameter { ParameterName = "@StatusTypeID", Value = GetDeviceStatus.StatusTypeId},
            };
            var result = await _appContext.GetDeviceStatus.FromSqlRaw($"exec spd_GetDeviceStatus @DeviceTypeID,@DeviceID,@StatusTypeID", parms.ToArray()).ToListAsync();
            return result;
        }
        public async Task<List<DeviceDetailsMaster>> GetDeviceMaster()
        {
            try
            {
                var result = await _appContext.GetDeviceMaster.FromSqlRaw($"exec spd_GetDeviceMaster").ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "GetDeviceMasterDetails";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }
            return null;
        }

        public async Task<List<ProblemCategorydata>> GetCategory()
        {
            try
            {
               
                var pcategory = await _appContext.GetCategory.FromSqlRaw($"exec spd_GetProblemCategory").ToListAsync();
               
                return pcategory;

            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "GetProblemCategory";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);
            }
            return null;
        }
    }
}