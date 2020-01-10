using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace testMultiTenants
{
    public interface ITenantConfiguration
    {
        string Tenant { get; }

        void Configure(IConfiguration configuration);
        void ConfigureServices(IServiceCollection services);

    }
}