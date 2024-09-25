using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace ConfigurationInfo.Security
{
    //This class use pipeline of the request and response of security
    public class RequestHeaderMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IConfiguration _config;

        public RequestHeaderMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            this._config = config;
        }

        public async Task Invoke(HttpContext context)

        {
            context.Request.EnableBuffering();

            var isSecure = "";
            var apiVersion = context.Request.Path.Value.Contains("v1");
            if (!apiVersion)
            {
                isSecure = _config.GetSection("secure").Value;
            }


            if (isSecure == "true")
            {
                string hashValue = context.Request.Headers["SignIt"];


                if (hashValue != null)
                {
                    var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();

                    if (hashValue == HashOperation.ComputeHmac256(body))
                    {


                        var stream = context.Request.Body;
                        var requestBodyDecrypted = AESOperation.DecryptString(body);

                        var requestData = Encoding.UTF8.GetBytes(requestBodyDecrypted);
                        stream = new MemoryStream(requestData);
                        context.Request.Body = stream;
                        await this.next.Invoke(context);

                    }
                    else
                    {
                        var jsonString = "{\"message\":\"Invalid request\"}";
                        var encrypted = AESOperation.EncryptString(jsonString);
                        byte[] data = Encoding.UTF8.GetBytes(encrypted);
                        context.Response.Headers.Add("SignIt", new[] { HashOperation.ComputeHmac256(encrypted) });
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;
                        await context.Response.Body.WriteAsync(data, 0, data.Length);

                    }
                }
                else
                {
                    context.Response.StatusCode = 403;
                }
            }
            else
            {
                await this.next.Invoke(context);

            }



        }
    }

    public static class AuthenticationMiddleWareExtensions
    {
        public static IApplicationBuilder UseMyCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestHeaderMiddleware>();
        }
    }

}
