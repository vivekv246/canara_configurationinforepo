using configurationinfo.Model;
using ConfigurationInfo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationInfo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using ConfigurationInfo.Utility;

namespace ConfigurationInfo.DatabaseContext
{
    public class ApplicationDBContext : DbContext
    {
        private readonly HttpContext _httpContext;
        private readonly IConfiguration _config;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IHttpContextAccessor httpContextAccessor = null, IConfiguration config = null) : base(options)
        {
            _httpContext = httpContextAccessor?.HttpContext;
            _config = config;
            Database.SetCommandTimeout(150000);
        }
        public DbSet<DeviceDetails> GetDeviceDetails { get; set; }
        public DbSet<DeviceStatusDetails> GetDeviceStatusDetails { get; set; }
        public DbSet<DeviceStatusModel> GetDeviceName { get; set; }
        public DbSet<Response> Response { get; set; }
        public DbSet<Response> InsertLogs { get; set; }
        public DbSet<TokenExpiry> TokenExpiry { get; set; }

        public DbSet<ProblemConfigration> GetProblemConfigInfos { get; set; }
        public DbSet<DeviceStatusType> GetDeviceStatusType { get; set; }
        public DbSet<DeviceStatusData> GetDeviceStatus { get; set; }
        public DbSet<DeviceDetailsMaster> GetDeviceMaster { get; set; }
        public DbSet<ProblemCategorydata> GetCategory { get; set; }
        public DbSet<Statustype> GetStatustypes { get; set; }
        public DbSet<DeviceTypeDetails> GetDeviceType { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var Path = _config.GetSection("File").GetSection("Path").Value.ToString();

                Database data = JsonFileReader.Read<Database>(Path);



                var connectionString = AESOperation.DecryptString(data.CMMS_Config_Master);
                optionsBuilder.UseSqlServer(connectionString);

                

            }
        }
    }
}
