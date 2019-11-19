using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using APIWarehouse.Context;
using APIWarehouse.Domains;
using APIWarehouse.Domains.Interface;
using APIWarehouse.Repository;
using APIWarehouse.Repository.Interface;

namespace APIWarehouse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
