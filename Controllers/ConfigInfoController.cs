// Decompiled with JetBrains decompiler
// Type: ConfigurationInfo.Controllers.ConfigInfoController
// Assembly: ConfigurationInfo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCD9E667-CFF5-418B-9E14-3CA80787717C
// Assembly location: C:\Users\pramodp\Downloads\PRAMOD\PRAMOD\ConfigurationInfo\ConfigurationInfo.dll

using ConfigurationInfo.Model;
using ConfigurationInfo.Repository.IRepository;
using ConfigurationInfo.Security;
using MasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.Json;
using System.Threading.Tasks;


#nullable enable
namespace ConfigurationInfo.Controllers
{
    [ApiController]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/ConfigInfo")]
    [EnableCors("AllowAllHeaders")]
  public class ConfigInfoController : ControllerBase
  {
    private 
    #nullable disable
    IConfigInfoRepository _configInfo;
    private ICommonRepository _crRepos;

    public ConfigInfoController(IConfigInfoRepository iconfigInfo, ICommonRepository commonRepo)
    {
      this._configInfo = iconfigInfo;
      this._crRepos = commonRepo;
    }

    [HttpGet("GetDeviceStatus")]
    [MiddlewareFilter(typeof (AuthorizationPipeLine))]
    public async Task<ActionResult<ConfigInfo>> GetDeviceStatus()
    {
      ConfigInfo result = await this._configInfo.GetDeviceStatus();
      try
      {
        if (result == null)
          return (ActionResult<ConfigInfo>) (ActionResult) this.NoContent();
        string json = JsonSerializer.Serialize<ConfigInfo>(result);
        return (ActionResult<ConfigInfo>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
      }
      catch (Exception ex)
      {
        this._crRepos.InsertErrorLog("", "ConfigInfo GetdeviceStatus() method error  ", ex.Message);
        return (ActionResult<ConfigInfo>) (ActionResult) this.NoContent();
      }
    }

    [HttpGet("GetProblem")]
    [MiddlewareFilter(typeof (AuthorizationPipeLine))]
    public async Task<ActionResult<ConfigInfo>> ProblemConfig()
    {
      ConfigInfo result = await this._configInfo.ProblemConfig();
      try
      {
        if (result == null)
          return (ActionResult<ConfigInfo>) (ActionResult) this.NoContent();
        string json = JsonSerializer.Serialize<ConfigInfo>(result);
        return (ActionResult<ConfigInfo>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
      }
      catch (Exception ex)
      {
        this._crRepos.InsertErrorLog("", "ConfigInfo GetProblem() method error  ", ex.Message);
        return (ActionResult<ConfigInfo>) (ActionResult) this.NoContent();
      }
    }

    public string EncryptedResponse(string result)
    {
      try
      {
        string data = AESOperation.EncryptString(result);
        this.HttpContext.Response.Headers.Add("SignIt", (StringValues) HashOperation.ComputeHmac256(data));
        return data;
      }
      catch (Exception ex)
      {
        return "";
      }
    }
  }
}
