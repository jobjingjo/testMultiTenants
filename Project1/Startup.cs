using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace testMultiTenants
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITenantIdentificationService, HostTenantIdentificationService>();
            services.Configure<TenantMapping>(options => Configuration.GetSection("Tenants").Bind(options));
            //services.Configure<>("abc", options =>
            //{
            //    options.NumberOption = 1;
            //    options.StringOption = "abc";
            //});

            //services.Configure("xyz", options =>
            //{
            //    options.NumberOption = 2;
            //    options.StringOption = "xyz";
            //});
            services.AddScoped<ITenantService, TenantService>();
            services.AddTenantConfiguration<ITenantConfiguration>();

            services.AddControllers();
            services.AddMvc();
            services.AddRazorPages();
            //// for having different Razor .cshtml files per tenant
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Insert(0, new TenantViewLocationExpander());
            });

            //// the tenants configuration
            //services.Configure(this.Configuration.GetSection("Tenants"));

            //// configuration specific for tenant abc
            //services.Configure("abc", options =>
            //{
            //    options.NumberOption = 1;
            //    options.StringOption = "abc";
            //});

            //// configuration specific for tenant xyz
            //services.Configure("xyz", options =>
            //{
            //    options.NumberOption = 2;
            //    options.StringOption = "xyz";
            //});

            //// a multitenant-aware DbContext
            //services.AddDbContext(options =>
            //{
            //    // the default connection, use whatever you need, if using the different connection per tenant strategy, this will be overridden
            //    options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            //});

            //// the tenant service, required by all the others, entry point for ITenantIdentificationService
            //services.AddScoped<ITenantService, TenantService>();

            //// the tenant identification/resolution service
            //services.AddScoped<ITenantIdentificationService, HostTenantIdentificationService>();

            //// the service for applying multitenancy to a multitenant-aware DbContext
            //services.AddSingleton<ITenantDbContext, FilterTenantDbContext>();

            //// adding tenant-specific configuration classes from a given assembly
            //services.AddTenantConfiguration();
            services.AddSingleton<ITenantDbContext, DifferentConnectionTenantDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
