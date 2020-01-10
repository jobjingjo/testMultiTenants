using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace testMultiTenants
{
    public sealed class QueryStringTenantIdentificationService : ITenantIdentificationService
    {
        private readonly TenantMapping _tenants;

        public QueryStringTenantIdentificationService(IOptions<TenantMapping> settings)
        {
            this._tenants = settings.Value;
        }

        public string GetCurrentTenant(HttpContext context)
        {
            var tenant = context.Request.Query["Tenant"].ToString();

            if (string.IsNullOrWhiteSpace(tenant) || !this._tenants.Tenants.ContainsKey(tenant))
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
}