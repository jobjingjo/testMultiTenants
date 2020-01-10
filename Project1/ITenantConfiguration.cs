public interface ITenantConfiguration
{
    void Configure(IConfiguration configuration);
    void ConfigureServices(IServiceCollection services);

}