using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationInfo.Security
{
    public class AuthorizationPipeLine
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthorizationMiddleware();
        }
    }
}
