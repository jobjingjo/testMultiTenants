using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace testMultiTenants
{
    public sealed class abcTenantConfiguration : ITenantConfiguration
    {
        public void Configure(IConfiguration configuration)
        {
            configuration["StringOption"] = "abc";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IMyService, XptoService>();
        }

        public string Tenant => "abc";
    }
}