public sealed class TenantViewLocationExpander : IViewLocationExpander
{
    private ITenantService _service;
    private string _tenant;

    public IEnumerable ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable 
        viewLocations)
    {
        foreach (var location in viewLocations)
        {
            yield return location.Replace("{0}", this._tenant + "/{0}");
            yield return location;
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
        this._service = context.ActionContext.HttpContext.RequestServices.GetService();
        this._tenant = this._service.GetCurrentTenant();
    }
}