namespace KanBanApi.Implementation
{
    public static class StartupExts
    {

        public static IServiceCollection ImplementApi(this IServiceCollection services)
        {
            return services;
        }

        public static IConfigurationBuilder SetConfiguration(this IConfigurationBuilder configuration)
        {
            return configuration;
        }
    }
}
