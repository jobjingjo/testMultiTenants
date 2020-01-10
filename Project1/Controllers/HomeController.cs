public HomeController(IOptionsSnapshot settings, ITenantService service)
{
    var tenant = service.GetCurrentTenant();
    var tenantSettings = settings.Get(tenant);
}