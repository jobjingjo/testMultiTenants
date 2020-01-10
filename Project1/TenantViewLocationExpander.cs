using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;

namespace testMultiTenants
{
    public sealed class TenantViewLocationExpander : IViewLocationExpander
    {
        private ITenantService _service;
        private string _tenant;

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string>
            viewLocations)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{0}", _tenant + "/{0}");
                yield return location;
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            _service = context.ActionContext.HttpContext.RequestServices.GetService<ITenantService>();
            _tenant = _service.GetCurrentTenant();
        }
    }
}