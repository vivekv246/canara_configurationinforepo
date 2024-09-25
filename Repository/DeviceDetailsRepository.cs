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
    public class DeviceDetailsRepository : IDeviceDetailsRepository
    {
        ApplicationDBContext _context;
        public DeviceDetailsRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<DeviceDetails>> GetDeviceDetails()
        {
            var result = await _context.GetDeviceDetails.FromSqlRaw($"exec spd_GetDeviceDetails").ToListAsync();
            return result;
        }

        public async Task<Response> InsertDeviceDetails(DeviceDetails InsertDeviceDetails)
        {
           Response response = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@Device", Value = InsertDeviceDetails.Device},
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = InsertDeviceDetails.DeviceTypeID },
                new SqlParameter { ParameterName = "@Description", Value = InsertDeviceDetails.Description },
                new SqlParameter { ParameterName = "@CreatedBy", Value = InsertDeviceDetails.CreatedBy }
               
            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_InsertDeviceDetails @Device, @DeviceTypeID, @Description, @CreatedBy", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "AddDeviceDetails";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return response;

        }
        public async Task<Response> UpdateDeviceDetails(DeviceDetails UpdateDeviceDetails)
        {
            Response response = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceID", Value = UpdateDeviceDetails.DeviceID},
                new SqlParameter { ParameterName = "@Device", Value = UpdateDeviceDetails.Device},
                new SqlParameter { ParameterName = "@DeviceTypeID", Value = UpdateDeviceDetails.DeviceTypeID },
                new SqlParameter { ParameterName = "@Description", Value = UpdateDeviceDetails.Description },
                new SqlParameter { ParameterName = "@ModifiedBy", Value = UpdateDeviceDetails.ModifiedBy }

            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_UpdateDeviceDetails @DeviceID,@Device, @DeviceTypeID, @Description, @ModifiedBy", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    response.Message = data.Message;
                    response.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "UpdateDeviceDetails";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return response;

        }
        public async Task<Response> DeleteDeviceDetails(DeviceDetails DeleteDeviceDetails)
        {

            Response commonResponse = new Response();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
                { 
                // Create parameters    
                new SqlParameter { ParameterName = "@DeviceId", Value = DeleteDeviceDetails.DeviceID}
            };
                var result = await _context.Response.FromSqlRaw(@"exec spd_DeleteDevice @DeviceId", parms.ToArray()).ToListAsync();
                foreach (var data in result)
                {
                    commonResponse.Message = data.Message;
                    commonResponse.StatusCode = data.StatusCode;
                };
            }
            catch (Exception ex)
            {
                Errorlog errorlog = new Errorlog();
                errorlog.Method = "DeleteDeviceDetails";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                await InsertErrorLog(errorlog);

            }
            return commonResponse;
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

        public async Task<List<DeviceTypeDetails>> GetDeviceType()
        {

            var result = await _context.GetDeviceType.FromSqlRaw($"exec spd_getDevicetype").ToListAsync();
          
            return result;
        }
    }
}
