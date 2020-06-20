using IMS.BusinessLayer;
using IMS.BusinessLayer.Interfaces;
using IMS.Filters;
using IMS.JWTAuth;
using IMS.JWTAuth.Interfaces;
using IMS.Models;
using IMS.Models.Interfaces;
using IMS.Repository;
using IMS.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IMSContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:IMSDB"]));
            services.AddControllers();
            services.AddScoped<IBLVendor, BLVendor>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IBLPurchaseOrder, BLPurchaseOrder>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                //options.Configuration = "localhost:6379";
                options.Configuration = Configuration["ConnectionString:REDIS"] ;
                options.InstanceName = "IMSInstance";

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "IMS API", Version = "v2" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //c.DocumentFilter<MyDocumentFilter>();
            });
            string key = "something from amitab and something from Dharmendra and finnlay something from mithun chkroberti if you like something from venod mehara also";
            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(
                x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };                      
                    }
                );
            
            services.AddSingleton<IJWTAuthManager>(new JWDAuthManager(key));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMS API");
            });
         
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
