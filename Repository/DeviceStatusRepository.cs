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
    public class DeviceStatusRepository : IDeviceStatusRepository
    {
        ApplicationDBContext _context;
        public DeviceStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<DeviceStatusDetails>> GetDeviceStatusDetails()
        {
            var result = await _context.GetDeviceStatusDetails.FromSqlRaw($"exec spd_DeviceStatusMaster").ToListAsync();
            return result;
        }

        public async Task<Response> InsertDeviceStatus(DeviceStatusDetails InsertDeviceStatus)
        {
            Response response = new Response();
            try
            {   
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@StatusTypeId", Value = InsertDeviceStatus.StatusTypeId },
                new SqlParameter { ParameterName = "@DeviceID", Value = InsertDeviceStatus.DeviceID },
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = InsertDeviceStatus.DeviceTypeID },
                new SqlParameter { ParameterName = "@DeviceStatus", Value = InsertDeviceStatus.DeviceStatus== null ? DBNull.Value :  InsertDeviceStatus.DeviceStatus },
                new SqlParameter { ParameterName = "@DeviceStatusSeverity", Value = InsertDeviceStatus.DeviceStatusSeverity== null ? DBNull.Value :  InsertDeviceStatus.DeviceStatusSeverity },
                new SqlParameter { ParameterName = "@DeviceStatusDescription", Value = InsertDeviceStatus.DeviceStatusDescription == null ? DBNull.Value :  InsertDeviceStatus.DeviceStatusDescription},
               

                new SqlParameter { ParameterName = "@No_Recurrences", Value = InsertDeviceStatus.No_Recurrences == null ? DBNull.Value :  InsertDeviceStatus.No_Recurrences },
                new SqlParameter { ParameterName = "@CreateTT", Value = InsertDeviceStatus.CreateTT == null ? DBNull.Value :  InsertDeviceStatus.CreateTT},
                new SqlParameter { ParameterName = "@CloseTT", Value = InsertDeviceStatus.CloseTT == null ? DBNull.Value :  InsertDeviceStatus.CloseTT},
                new SqlParameter { ParameterName = "@IgnoreTT", Value = InsertDeviceStatus.IgnoreTT == null ? DBNull.Value :  InsertDeviceStatus.IgnoreTT},
                new SqlParameter { ParameterName = "@CreatedBy", Value = InsertDeviceStatus.CreatedBy  == null ? DBNull.Value :  InsertDeviceStatus.CreatedBy}

            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_InsertDeviceStatus @StatusTypeId,@DeviceID,@DeviceTypeID,@DeviceStatus,@DeviceStatusSeverity,@DeviceStatusDescription,@No_Recurrences,@CreateTT,@CloseTT,@IgnoreTT,@CreatedBy", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "AddDeviceStatus";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return response;
        }
        public async Task<Response> UpdateDeviceStatus(DeviceStatusDetails UpdateDeviceStatus)
        {
            Response response = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters
                new SqlParameter { ParameterName = "@DeviceStatusID", Value = UpdateDeviceStatus.DeviceStatusID},
                new SqlParameter { ParameterName = "@StatusTypeId", Value = UpdateDeviceStatus.StatusTypeId},
                new SqlParameter { ParameterName = "@DeviceID", Value = UpdateDeviceStatus.DeviceID },
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = UpdateDeviceStatus.DeviceTypeID },
                new SqlParameter { ParameterName = "@DeviceStatus", Value = UpdateDeviceStatus.DeviceStatus  == null ? DBNull.Value :  UpdateDeviceStatus.DeviceStatus},
                new SqlParameter { ParameterName = "@DeviceStatusSeverity", Value = UpdateDeviceStatus.DeviceStatusSeverity== null ? DBNull.Value :  UpdateDeviceStatus.DeviceStatusSeverity },
                new SqlParameter { ParameterName = "@DeviceStatusDescription", Value = UpdateDeviceStatus.DeviceStatusDescription == null ? DBNull.Value :  UpdateDeviceStatus.DeviceStatusDescription},


                new SqlParameter { ParameterName = "@No_Recurrences", Value = UpdateDeviceStatus.No_Recurrences  == null ? DBNull.Value :  UpdateDeviceStatus.No_Recurrences},
                new SqlParameter { ParameterName = "@CreateTT", Value = UpdateDeviceStatus.CreateTT == null ? DBNull.Value :  UpdateDeviceStatus.CreateTT},
                new SqlParameter { ParameterName = "@CloseTT", Value = UpdateDeviceStatus.CloseTT == null ? DBNull.Value :  UpdateDeviceStatus.CloseTT},
                new SqlParameter { ParameterName = "@IgnoreTT", Value = UpdateDeviceStatus.IgnoreTT == null ? DBNull.Value :  UpdateDeviceStatus.IgnoreTT},
                new SqlParameter { ParameterName = "@ModifiedBy", Value = UpdateDeviceStatus.ModifiedBy  == null ? DBNull.Value :  UpdateDeviceStatus.ModifiedBy}

            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_UpdateDeviceStatus @DeviceStatusID,@StatusTypeId,@DeviceID,@DeviceTypeID,@DeviceStatus,@DeviceStatusSeverity,@DeviceStatusDescription,@No_Recurrences,@CreateTT,@CloseTT,@IgnoreTT,@ModifiedBy", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "UpdateDeviceStatus";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return response;
        }
        public async Task<Response> DeleteDeviceStatus(DeviceStatusDetails DeleteDeviceStatus)
        {
            Response commonResponse = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceStatusID", Value = DeleteDeviceStatus.DeviceStatusID}
            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_DeleteDeviceStatus @DeviceStatusID", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    commonResponse.Message = data.Message;
                    commonResponse.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "DeleteDeviceStatus";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return commonResponse;
        }
        public async Task<List<DeviceStatusModel>> GetDeviceName(DeviceStatusModel GetDeviceName)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = GetDeviceName.DeviceTypeID},

            };
            var result = await _context.GetDeviceName.FromSqlRaw($"exec spd_GetDeviceName @DeviceTypeID", parms.ToArray()).ToListAsync();
            return result;
        }

        public Task<int> InsertErrorLog(Errorlog errorlog)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@KioskID", Value = errorlog.KioskID},
                new SqlParameter { ParameterName = "@Method", Value = errorlog.Method},
                new SqlParameter { ParameterName = "@ErrorDetail", Value = errorlog.ErrorDetail}
            };
            int result = _context.Database.ExecuteSqlRaw(@"exec SP_INS_ErrorLog @KioskID , @Method , @ErrorDetail", parms.ToArray());
            return Task.FromResult(result);
        }

        public async Task<List<Statustype>> GetStatustypes()
        {
            var result = await _context.GetStatustypes.FromSqlRaw($"exec spd_ShowDeviceStatusType").ToListAsync();
            return result;
        }
    }
}
