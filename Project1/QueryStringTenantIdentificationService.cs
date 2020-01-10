public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService
{
    private readonly TenantMapping _tenants;

    public QueryStringTenantIdentificationService(IConfiguration configuration)
    {
        this._tenants = configuration.GetTenantMapping();
    }

    public string GetCurrentTenant(HttpContext context)
    {
        var tenant = context.Request.Query["Tenant"].ToString();

        if (string.IsNullOrWhiteSpace(tenant) || !this._tenants.Tenants.Values.Contains(tenant, 
            StringComparer.InvariantCultureIgnoreCase))
        {
            return this._tenants.Default;
        }

        if (this._tenants.Tenants.TryGetValue(tenant, out var mappedTenant))
        {
            return mappedTenant;
        }

        return tenant;
    }
}