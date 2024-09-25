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
    [ApiVersion("v2")]
    [Route("{v:apiVersion}/[Controller]")]
    [EnableCors("AllowAllHeaders")]
    public class ConfigInfoV2Controller : ControllerBase
    {
        private IConfigInfoRepository _configInfo;
        private ICommonRepository _crRepos;

        private readonly IConfiguration _config;
        public ConfigInfoV2Controller(IConfigInfoRepository iconfigInfo, ICommonRepository commonRepo, IConfiguration config)
        {

            _configInfo = iconfigInfo;
            _crRepos = commonRepo;
            _config = config;

        }


        [HttpGet("GetDeviceStatus")]
        [MiddlewareFilter(typeof(AuthorizationPipeLine))]


        public async Task<ActionResult<ConfigInfo>> GetDeviceStatus()
        {
            var result = await _configInfo.GetDeviceStatus();
            try
            {
                if (result != null)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok((EncryptedResponse(json)));
                    //return Ok(json);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _crRepos.InsertErrorLog("", "ConfigInfo GetdeviceStatus() method error  ", ex.Message);
                return NoContent();
            }


        }
        [HttpGet("GetProblem")]
        [MiddlewareFilter(typeof(AuthorizationPipeLine))]
        public async Task<ActionResult<ConfigInfo>> ProblemConfig()
        {
            var result = await _configInfo.ProblemConfig();
            try
            {
                if (result != null)
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok((EncryptedResponse(json)));
                    // return Ok(json);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _crRepos.InsertErrorLog("", "ConfigInfo GetProblem() method error  ", ex.Message);
                return NoContent();
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
                _crRepos.InsertErrorLog("", "ConfigInfo GetdeviceStatus() method error  ", ex.Message);
                return "";

            }


        }

    }
}
