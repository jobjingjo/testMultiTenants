using Microsoft.AspNetCore.Http;

namespace testMultiTenants
{
    public interface ITenantIdentificationService
    {
        string GetCurrentTenant(HttpContext httpContext);
    }
}