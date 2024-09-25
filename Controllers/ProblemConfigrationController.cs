// Decompiled with JetBrains decompiler
// Type: configurationinfo.Controllers.ProblemConfigrationController
// Assembly: ConfigurationInfo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCD9E667-CFF5-418B-9E14-3CA80787717C
// Assembly location: C:\Users\pramodp\Downloads\PRAMOD\PRAMOD\ConfigurationInfo\ConfigurationInfo.dll

using configurationinfo.Model;
using ConfigurationInfo.Model;
using configurationinfo.Repository.IRepository;
using ConfigurationInfo.Security;
using MasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;


#nullable enable
namespace configurationinfo.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/ProblemConfigration")]
    [EnableCors("AllowAllHeaders")]
  public class ProblemConfigrationController : ControllerBase
  {
    private readonly 
    #nullable disable
    IProblemConfigration _problemConfigration;
    private readonly ICommonRepository _commonRepository;

    public ProblemConfigrationController(
      IProblemConfigration problemConfigration,
      ICommonRepository commonRepository)
    {
      this._problemConfigration = problemConfigration;
      this._commonRepository = commonRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ProblemConfigration>> GetProblemConfigrationInformation()
    {
      try
      {
        List<ProblemConfigration> result = await this._problemConfigration.GetProblemConfigrationInformation();
        if (result.Count <= 0)
          return (ActionResult<ProblemConfigration>) (ActionResult) this.NoContent();
        string json = JsonSerializer.Serialize<List<ProblemConfigration>>(result);
        return (ActionResult<ProblemConfigration>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController GetProblemConfigrationInformation() method error  ", ex.Message);
      }
      return (ActionResult<ProblemConfigration>) (ActionResult) this.NoContent();
    }

    [HttpGet("GetDeviceMaster")]
    public async Task<ActionResult<DeviceDetailsMaster>> GetDeviceMaster()
    {
      try
      {
        List<DeviceDetailsMaster> result = await this._problemConfigration.GetDeviceMaster();
        if (result.Count <= 0)
          return (ActionResult<DeviceDetailsMaster>) (ActionResult) this.NoContent();
        string json = JsonSerializer.Serialize<List<DeviceDetailsMaster>>(result);
        return (ActionResult<DeviceDetailsMaster>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController GetProblemConfigrationInformation() method error  ", ex.Message);
      }
      return (ActionResult<DeviceDetailsMaster>) (ActionResult) this.NoContent();
    }

    [HttpPost]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<ConfigurationInfo.Model.Response>> AddProblemConfigration(
      ProblemConfigration problemConfigration)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._problemConfigration.InsertProblemConfigration(problemConfigration);
        if (result != null)
        {
          string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<ConfigurationInfo.Model.Response>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
        }
        string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<ConfigurationInfo.Model.Response>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json1));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController AddProblemConfigration() method error  ", ex.Message);
      }
      return (ActionResult<ConfigurationInfo.Model.Response>) (ActionResult) this.NoContent();
    }

    [HttpPut]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> UpdateProblemConfigration(
      ProblemConfigration problemConfigration)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._problemConfigration.UpdateProblemConfigration(problemConfigration);
        if (result != null)
        {
          string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
        }
        string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json1));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController UpdateProblemConfigration() method error  ", ex.Message);
      }
      return (ActionResult<string>) (ActionResult) this.NoContent();
    }

    [HttpDelete]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> DeleteProblemConfigration(
      ProblemConfigration problemConfigration)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._problemConfigration.DeleteProblemConfigration(problemConfigration.ProblemID);
        if (result != null)
        {
          string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
        }
        string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json1));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController DeleteProblemConfigration() method error  ", ex.Message);
      }
      return (ActionResult<string>) (ActionResult) this.NoContent();
    }

    [HttpPost("GetDeviceStatusType")]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> GetDeviceStatusType(DeviceStatusType GetDeviceStatusType)
    {
      try
      {
        List<DeviceStatusType> result = await this._problemConfigration.GetDeviceStatusType(GetDeviceStatusType);
        if (result != null)
        {
          string json = JsonSerializer.Serialize<List<DeviceStatusType>>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
        }
        string json1 = JsonSerializer.Serialize<List<DeviceStatusType>>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json1));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController GetDeviceStatusType() method error  ", ex.Message);
      }
      return (ActionResult<string>) (ActionResult) this.NoContent();
    }

    [HttpPost("GetDeviceStatus")]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> GetDeviceStatus(DeviceStatusData GetDeviceStatus)
    {
      try
      {
        List<DeviceStatusData> result = await this._problemConfigration.GetDeviceStatus(GetDeviceStatus);
        if (result != null)
        {
          string json = JsonSerializer.Serialize<List<DeviceStatusData>>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
        }
        string json1 = JsonSerializer.Serialize<List<DeviceStatusData>>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json1));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "ProblemConfigrationController GetDeviceStatus() method error  ", ex.Message);
      }
      return (ActionResult<string>) (ActionResult) this.NoContent();
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
        Errorlog errorlog = new Errorlog()
        {
          Method = "Encryption Response",
          ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString()
        };
        return "";
      }
    }
  }
}
