using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWarehouse.Context;
using APIWarehouse.Domains;
using APIWarehouse.Domains.Interface;
using APIWarehouse.Repository;
using APIWarehouse.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace APIWarehouse
{
    public class StartupMigration
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        private readonly ILogger _logger;
        
        public StartupMigration(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("New ApiCadastro Instance para migrations. Env: {EnvironmentName}", HostingEnvironment.EnvironmentName);

            string sqlserver = Configuration.GetConnectionString("SqlServerWatchguard");
            _logger.LogInformation("SQL Server Connection String: {SqlServerWatchguard}", sqlserver);
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(sqlserver), ServiceLifetime.Transient);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }
         
    }
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        private readonly ILogger _logger;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("New APIWrehouse Instance. Env: {EnvironmentName}", HostingEnvironment.EnvironmentName);
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            
            services.AddTransient<IBrandDomain, BrandDomain>();
            services.AddTransient<IProductDomain, ProductDomain>();

            string sqlserver = Configuration.GetConnectionString("SqlServerWatchguard");
            _logger.LogInformation("SQL Server Connection String: {SqlServerWatchguard}", sqlserver);
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(sqlserver), ServiceLifetime.Transient);

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<WarehouseContext>();
            context.Database.Migrate();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(MyAllowSpecificOrigins);

            app.UseMvc();
        }
    }
}
