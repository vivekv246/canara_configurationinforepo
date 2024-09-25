using System;
using System.Text.Json;
using System.Threading.Tasks;
using configurationinfo.Model;
using configurationinfo.Repository.IRepository;
using ConfigurationInfo.Model;
using ConfigurationInfo.Security;
using MasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace configurationinfo.Controllers
{

    [ApiController]
    [ApiVersion("2")]
    [Route("v{v:apiVersion}/ProblemConfigration")]
    [EnableCors("AllowAllHeaders")]
    public class ProblemConfigrationV2Controller : ControllerBase
    {

        private readonly IProblemConfigration _problemConfigration;
        private readonly ICommonRepository _commonRepository;
        private readonly IConfiguration _config;
        public ProblemConfigrationV2Controller(IProblemConfigration problemConfigration, ICommonRepository commonRepository,IConfiguration config)
        {

            _problemConfigration = problemConfigration;
            _commonRepository = commonRepository;
            _config = config ;

        }


        [HttpGet]
       // [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        public async Task<ActionResult<ProblemConfigration>> GetProblemConfigrationInformation()
        {
            try
            {
                var result = await _problemConfigration.GetProblemConfigrationInformation();
                if (result.Count > 0)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController GetProblemConfigrationInformation() method error  ", ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetDeviceMaster")]
      //  [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        public async Task<ActionResult<DeviceDetailsMaster>> GetDeviceMaster()
        {
            try
            {
                var result = await _problemConfigration.GetDeviceMaster();
                if (result.Count > 0)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));
                    // return Ok(result);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController GetProblemConfigrationInformation() method error  ", ex.Message);
            }

            return NoContent();
        }
        [HttpPost]
      //  [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<Response>> AddProblemConfigration(ProblemConfigration problemConfigration)
        {
            try
            {
                var result = await _problemConfigration.InsertProblemConfigration(problemConfigration);
                if (result != null)
                {

                   var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));


                }
                else
                {
                    var json = JsonSerializer.Serialize(result);
                    return NotFound(EncryptedResponse(json));

                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController AddProblemConfigration() method error  ", ex.Message);
            }

            return NoContent();
        }


        [HttpPut]
     //   [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> UpdateProblemConfigration(ProblemConfigration problemConfigration)
        {
            try
            {
                var result = await _problemConfigration.UpdateProblemConfigration(problemConfigration);
                if (result != null)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                    return Ok(EncryptedResponse(json));
                }
                else
                {
                      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));

                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController UpdateProblemConfigration() method error  ", ex.Message);
            }
            return NoContent();
        }



        [HttpDelete]
     //   [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> DeleteProblemConfigration(ProblemConfigration problemConfigration)
        {
            try
            {
                var result = await _problemConfigration.DeleteProblemConfigration(problemConfigration.ProblemID);
                if (result != null)
                {
                      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));

                }
                else
                {
                      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));
                   
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController DeleteProblemConfigration() method error  ", ex.Message);
            }
            return NoContent();
        }

        [HttpPost("GetDeviceStatusType")]
     //   [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> GetDeviceStatusType(DeviceStatusType GetDeviceStatusType)
        {
            try
            {
                var result = await _problemConfigration.GetDeviceStatusType(GetDeviceStatusType);
                if (result != null)
                {

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));
                 

                }
                else
                {
                      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));

                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController GetDeviceStatusType() method error  ", ex.Message);
            }

            return NoContent();
        }
        [HttpPost("GetDeviceStatus")]
       // [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        [MiddlewareFilter(typeof(AuthenticationMiddlewarePipeline))]
        public async Task<ActionResult<String>> GetDeviceStatus(DeviceStatusData GetDeviceStatus)
        {
            try
            {
                var result = await _problemConfigration.GetDeviceStatus(GetDeviceStatus);
                if (result != null)
                {

                     var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(EncryptedResponse(json));


                }
                else
                {
                     var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return NotFound(EncryptedResponse(json));
                   // return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _commonRepository.InsertErrorLog("", "ProblemConfigrationController GetDeviceStatus() method error  ", ex.Message);
            }

            return NoContent();
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

                
                _commonRepository.InsertErrorLog("", "ConfigInfo  method error  ", ex.Message);
                return "";

            }


        }
    }
}