using IMS.BusinessLayer;
using IMS.BusinessLayer.Interfaces;
using IMS.Models;
using IMS.Models.Interfaces;
using IMS.Repository;
using IMS.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMS API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMS API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}