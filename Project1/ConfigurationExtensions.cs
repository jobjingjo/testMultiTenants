using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static TenantMapping GetTenantMapping(this IConfiguration configuration)
    {
        return configuration.GetSection("Tenants") as TenantMapping;
    }
}