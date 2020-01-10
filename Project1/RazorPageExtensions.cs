using Microsoft.AspNetCore.Mvc.Razor;

public static class RazorPageExtensions
{
    public static T GetValueForTenant(this IRazorPage page, string setting, T defaultValue = default(T))
    {
        var service = page.ViewContext.HttpContext.RequestServices.GetService();
        var tenant = service.GetCurrentTenant();
        var configuration = page.ViewContext.HttpContext.RequestServices.GetService();
        var section = configuration.GetSection("Tenants").GetSection(tenant);

        if (section.Exists())
        {
            return section.GetValue(setting, defaultValue);
        }
        else
        {
            return configuration.GetValue(setting, defaultValue);
        }
    }
}