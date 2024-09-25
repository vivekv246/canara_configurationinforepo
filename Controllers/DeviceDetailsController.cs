// Decompiled with JetBrains decompiler
// Type: ConfigurationInfo.Controllers.DeviceDetailsController
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
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;


#nullable enable
namespace ConfigurationInfo.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/DeviceDetails")]
    [EnableCors("AllowAllHeaders")]
  public class DeviceDetailsController : ControllerBase
  {
    private 
    #nullable disable
    IDeviceDetailsRepository _devicerepos;
    private readonly ICommonRepository _commonRepository;

    public DeviceDetailsController(
      IDeviceDetailsRepository deviceRepo,
      ICommonRepository commonRepository)
    {
      this._commonRepository = commonRepository;
      this._devicerepos = deviceRepo;
    }

    [HttpGet]
    [MiddlewareFilter(typeof (AuthorizationPipeLine))]
    public async Task<ActionResult<string>> GetDeviceDetails()
    {
      try
      {
        List<DeviceDetails> result = await this._devicerepos.GetDeviceDetails();
        if (result.Count <= 0)
          return (ActionResult<string>) (ActionResult) this.NoContent();
        string json = JsonSerializer.Serialize<List<DeviceDetails>>(result);
        return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "DeviceDetailsController GetDeviceStatus() method error  ", ex.Message);
        return (ActionResult<string>) (ActionResult) this.StatusCode(500);
      }
    }

    [HttpPost]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> InsertDeviceDetails(DeviceDetails InsertDeviceDetails)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._devicerepos.InsertDeviceDetails(InsertDeviceDetails);
        if (result != null)
        {
          if (result.StatusCode == "00" && result.Message != null)
          {
            string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
            return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
          }
          string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json1));
        }
        string json2 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json2));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "DeviceDetailsController InsertDeviceStatus() method error  ", ex.Message);
        return (ActionResult<string>) (ActionResult) this.StatusCode(500);
      }
    }

    [HttpPut]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> UpdateDeviceDetails(DeviceDetails UpdateDeviceDetails)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._devicerepos.UpdateDeviceDetails(UpdateDeviceDetails);
        if (result != null)
        {
          if (result.StatusCode == "00" && result.Message != null)
          {
            string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
            return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
          }
          string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json1));
        }
        string json2 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json2));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "DeviceDetailsController UpdateDeviceStatus() method error  ", ex.Message);
        return (ActionResult<string>) (ActionResult) this.StatusCode(500);
      }
    }

    [HttpDelete]
    [MiddlewareFilter(typeof (AuthenticationMiddlewarePipeline))]
    public async Task<ActionResult<string>> DeleteDeviceDetails(DeviceDetails DeleteDeviceDetails)
    {
      try
      {
        ConfigurationInfo.Model.Response result = await this._devicerepos.DeleteDeviceDetails(DeleteDeviceDetails);
        if (result != null)
        {
          if (result.StatusCode == "00" && result.Message != null)
          {
            string json = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
            return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json));
          }
          string json1 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
          return (ActionResult<string>) (ActionResult) this.Ok((object) this.EncryptedResponse(json1));
        }
        string json2 = JsonSerializer.Serialize<ConfigurationInfo.Model.Response>(result);
        return (ActionResult<string>) (ActionResult) this.NotFound((object) this.EncryptedResponse(json2));
      }
      catch (Exception ex)
      {
        this._commonRepository.InsertErrorLog("", "DeviceDetailsController DeleteDeviceStatus() method error  ", ex.Message);
        return (ActionResult<string>) (ActionResult) this.StatusCode(500);
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
        Errorlog errorlog = new Errorlog()
        {
          Method = "ConfigurationInfo",
          ErrorDetail = "Error:-" + ex.Message.ToString() + " Stack:-" + ex.StackTrace.ToString()
        };
        return "";
      }
    }
  }
}
