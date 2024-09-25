using ConfigurationInfo.Model;
using ConfigurationInfo.Repository.IRepository;
using ConfigurationInfo.Security;
using MasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationInfo.Controllers
{
    [ApiController]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/DeviceStatus")]
    [EnableCors("AllowAllHeaders")]
    public class DeviceStatusV2Controller : ControllerBase
    {
        private IDeviceStatusRepository _devicerepos;
        private readonly ICommonRepository _commonRepository;

        private readonly IConfiguration _config;

        public DeviceStatusV2Controller(IDeviceStatusRepository deviceRepo, ICommonRepository commonRepository, IConfiguration config)

        {
            _commonRepository = commonRepository;
            _devicerepos = deviceRepo;
            _config = config ;
           

        }
        [HttpGet]
   //   [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        public async Task<ActionResult<String>> GetDeviceStatusDetails()
        {
            try
            {
                var result = await _devicerepos.GetDeviceStatusDetails();
                if (result.Count > 0)
                {
                  var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                   return Ok(EncryptedResponse(json));
                     //return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "DeviceStatusController GetDeviceStatus() method error  ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
       // [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> InsertDeviceStatus(DeviceStatusDetails InsertDeviceStatus)
        {
            try
            {
                var result = await _devicerepos.InsertDeviceStatus(InsertDeviceStatus);
                if (result != null)
                {
                    if (result.StatusCode == "00" && result.Message != null)
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        return Ok(EncryptedResponse(json));
                       // return Ok(result);
                    }
                    else
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        return Ok(EncryptedResponse(json));
                       // return BadRequest(result);
                    }
                }
                else
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));
                   // return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "DeviceStatusController InsertDeviceStatus() method error  ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
      //  [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> UpdateDeviceStatus(DeviceStatusDetails UpdateDeviceStatus)
        {
            try
            {
                var result = await _devicerepos.UpdateDeviceStatus(UpdateDeviceStatus);
                if (result != null)
                {
                    if (result.StatusCode == "00" && result.Message != null)
                    {
                       var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                         return Ok(EncryptedResponse(json));
                       // return Ok(result);
                    }
                    else
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        return Ok(EncryptedResponse(json));
                      //  return BadRequest(result);
                    }
                }
                else
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));
                    //return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "DeviceStatusController UpdateDeviceStatus() method error  ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
      //  [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> DeleteDeviceStatus(DeviceStatusDetails DeleteDeviceStatus)
        {
            try
            {
                var result = await _devicerepos.DeleteDeviceStatus(DeleteDeviceStatus);
                if (result != null)
                {
                    if (result.StatusCode == "00" && result.Message != null)
                    {
                       var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        return Ok(EncryptedResponse(json));
                       // return Ok(result);
                    }
                    else
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        return Ok(EncryptedResponse(json));
                      //  return BadRequest(result);
                    }
                }
                else
                {
                     var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));
                   // return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "DeviceStatusController DeleteDeviceStatus() method error  ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("Getdevicename")]
       // [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> GetDeviceName(DeviceStatusModel GetDeviceName)
        {
            try
            {
                var result = await _devicerepos.GetDeviceName(GetDeviceName);
                if (result.Count > 0)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));
                    //return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "DeviceStatusController Getdevicename() method error  ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public string EncryptedResponse(String result)
        {
           try
            {
                var isSecure = _config.GetSection("secure").Value;
                if (isSecure == "false")
                {
                    return result;
                }
                else
                {
                    var responseBody = AESOperation.EncryptString(result);
                    HttpContext.Response.Headers.Add("SignIt", HashOperation.ComputeHmac256(responseBody));
                    Console.WriteLine("Successful");
                    return responseBody;
                }

            }
            catch (Exception ex)
            {

                Errorlog errorlog = new Errorlog();
                errorlog.Method = "JobDetailsController~EncryptedResponse";
                errorlog.ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString();
                _commonRepository.InsertErrorLog("", "ConfigInfo GetdeviceStatus() method error  ", ex.Message);
                return "";

            }


        }
    }
}
