using System.Text;
using System.Threading.Tasks;
using ConfigurationInfo.DatabaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace ConfigurationInfo.Security
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private ApplicationDBContext _dbContext;
        private readonly IConfiguration _config;

        public static string _authorizationToken;

        public AuthorizationMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            this._config = config;
        }

        public async Task Invoke(HttpContext context, ApplicationDBContext dbContext)

        {
            this._dbContext = dbContext;

            context.Request.EnableBuffering();

            var isSecure = "";
            var apiVersion = context.Request.Path.Value.Contains("v1");
            if (!apiVersion)
            {
                isSecure = _config.GetSection("secure").Value;
            }


            if (isSecure == "true")
            {
                string AuthorizationValue = context.Request.Headers["Authorization"];
                AuthorizationValue = AESOperation.DecryptString(AuthorizationValue);


                if (AuthorizationValue != null)
                {

                    var jwtToken = _config.GetSection("JwtConfig").GetSection("Secret").Value.ToString();
                    if (!TokenUtility.isTokenExpired(AuthorizationValue, _dbContext, jwtToken))
                    {
                        _authorizationToken = AuthorizationValue;
                        await this.next.Invoke(context);
                    }
                    else
                    {
                        var jsonString = "{\"message\":\"Token expired\"}";
                        var encrypted = AESOperation.EncryptString(jsonString);
                        byte[] data = Encoding.UTF8.GetBytes(encrypted);
                        context.Response.Headers.Add("SignIt", new[] { HashOperation.ComputeHmac256(encrypted) });
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 403;
                        await context.Response.Body.WriteAsync(data, 0, data.Length);
                    }


                }
                else
                {
                    var jsonString = "{\"message\":\"Authorization key missing in headers\"}";
                    var encrypted = AESOperation.EncryptString(jsonString);
                    byte[] data = Encoding.UTF8.GetBytes(encrypted);
                    context.Response.Headers.Add("SignIt", new[] { HashOperation.ComputeHmac256(encrypted) });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 403;
                    await context.Response.Body.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {

                await this.next.Invoke(context);
            }





        }




    }

    public static class AuthorizationMiddleWareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
