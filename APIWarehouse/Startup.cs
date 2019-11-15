using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWarehouse.Context;
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

namespace APIWarehouse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();        

            string sqlserver = Configuration.GetConnectionString("SqlServerWatchguard");
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(sqlserver));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<WarehouseContext>();
            context.Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    // public class StartupMigration
    // {
    //     public IConfiguration Configuration { get; }
    //     public IHostingEnvironment HostingEnvironment { get; }
        
    //     public StartupMigration(IConfiguration configuration, IHostingEnvironment env)
    //     {
    //         Configuration = configuration;
    //         HostingEnvironment = env;
    //     }

    //     public void ConfigureServices(IServiceCollection services)
    //     {
    //         string sqlserver = Configuration.GetConnectionString("SqlServerBilhetagem");
    //         services.AddDbContext<WatchguardContext>(options => options.UseSqlServer(sqlserver), ServiceLifetime.Transient);
    //     }

    //     public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //     {
    //     }
         
    // }
}
