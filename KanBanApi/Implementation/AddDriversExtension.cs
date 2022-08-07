using KanBanApi.Drivers;

namespace KanBanApi.Implementation
{
    public static class AddDriversExtension
    {
        public static IServiceCollection AddDrivers(
                this IServiceCollection services
            )
        {
            services.AddScoped<BoardDriver>();
            services.AddScoped<UserDriver>();

            services.AddScoped<BoardRepository>();

            return services;
        }
    }
}
