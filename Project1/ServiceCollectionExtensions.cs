public static class ServiceCollectionExtensions
{
        public static IServiceCollection AddTenantConfiguration(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(type => typeof(ITenantConfiguration).IsAssignableFrom(type))
                .Where(type => (type.IsAbstract == false) && (type.IsInterface == false));

            services.AddScoped(typeof(ITenantConfiguration), sp =>
            {
                var svc = sp.GetRequiredService<ITenantService>();
                var configuration = sp.GetRequiredService<IConfiguration>();
                var tenant = svc.GetCurrentTenant();
                var instance = types
                    .Select(type => ActivatorUtilities.CreateInstance(sp, type))
                    .OfType<ITenantConfiguration>()
                    .SingleOrDefault(x => x.Tenant == tenant);

                if (instance != null)
                {
                    instance.Configure(configuration);
                    instance.ConfigureServices(services);

                    sp.GetRequiredService<IHttpContextAccessor>().HttpContext.RequestServices = services.BuildServiceProvider();
                    return instance;
                }
                else
                {
                    return DummyTenantServiceProviderConfiguration.Instance;
                }
            });

            return services;
        }

        public static IServiceCollection AddTenantConfiguration<T>(this IServiceCollection services)
        {
            var assembly = typeof(T).Assembly;
            return services.AddTenantConfiguration(assembly);
        }
}