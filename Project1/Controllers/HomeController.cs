using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace testMultiTenants.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController:Controller
    {
        public HomeController(IOptions<TenantMapping> settings, ITenantService service)
        {
            var tenant = service.GetCurrentTenant();
        
            var tenantSettings = settings.Value.Tenants[tenant];
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}