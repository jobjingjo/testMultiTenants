public static class ConfigurationExtensions
{
    public static TenantMapping GetTenantMapping(this IConfiguration configuration)
    {
        return configuration.GetSection("Tenants").Get();
    }
}