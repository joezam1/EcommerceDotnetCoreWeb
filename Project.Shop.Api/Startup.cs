using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Shop.DataAccess.EFContext;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.Repositories;

using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.BusinessLogic.Helpers;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace Project.Shop.Api
{
    public class Startup
    {

        readonly string corsPolicyOrigins = "corsPolicyOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Database Configuration
            var connectionString = Configuration["ConnectionString:DefaultDatabase"];
            services.AddDbContext<DataContext>(opts=>opts.UseSqlServer(connectionString));
            services.AddScoped<DbContext, DataContext>();
            


            //Interfaces and Dependency Injection
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISizeService , SizeService>();
            services.AddScoped<IStatusService , StatusService>();
            services.AddScoped<IShopService , ShopService>();
            services.AddScoped<IShopRepository,ShopRepository>();
            services.AddScoped<IJsonHelper, JsonHelper>();


            services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate();

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3080",
                                        "http://www.example.com")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
           
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            //app.UseHttpsRedirection(); //This line blocks CORS from working OK

            app.UseRouting();// first
            // Use the CORS policy
             app.UseCors(corsPolicyOrigins);// second

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
