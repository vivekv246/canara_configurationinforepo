
using configurationinfo.Repository;
using configurationinfo.Repository.IRepository;
using ConfigurationInfo.DatabaseContext;
using ConfigurationInfo.Repository;
using ConfigurationInfo.Repository.IRepository;
using ConfigurationInfo.Utility;
using MasterAPI.Repository.Common;
using MasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ConfigurationInfo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<ApplicationDBContext>();
            // services.AddDbContext<ApplicationDBContext>(options =>
            // {
            //     options.UseSqlServer(Configuration.GetConnectionString("Defaultconnection"));
            // });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                //options.ApiVersionReader = ApiVersionReader.Combine(
                //    new QueryStringApiVersionReader("api-version"),
                //    new HeaderApiVersionReader("api-version"));
            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddScoped<IDeviceDetailsRepository, DeviceDetailsRepository>();
            services.AddScoped<IDeviceStatusRepository, DeviceStatusRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IProblemConfigration, ProblemConfigrationRepository>();
            services.AddScoped<IConfigInfoRepository, ConfigInfoRepository>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                          // This below line use for the force fully expose the custom header in response
                          .WithExposedHeaders("SignIt");


                        });
            });
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConfigurationInfo v1"));

            }
            app.UseSwagger();
            app.UseSwaggerUI(
            options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllHeaders");
            app.UsePreflightRequestHandler();
           // app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
