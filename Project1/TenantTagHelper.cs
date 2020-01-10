using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace testMultiTenants
{
    [HtmlTargetElement("tenant")]
    public sealed class TenantTagHelper : TagHelper
    {
        private readonly ITenantService _service;

        public TenantTagHelper(ITenantService service)
        {
            this._service = service;
        }

        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tenant = this.Name ?? string.Empty;

            if (tenant != this._service.GetCurrentTenant())
            {
                output.SuppressOutput();
            }

            return base.ProcessAsync(context, output);
        }
    }
}