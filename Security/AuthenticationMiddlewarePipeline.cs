using Microsoft.AspNetCore.Builder;


namespace ConfigurationInfo.Security
{
    public class AuthenticationMiddlewarePipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMyCustomAuthentication();
        }
    }
}
