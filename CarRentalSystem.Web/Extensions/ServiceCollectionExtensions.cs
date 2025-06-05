using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Services;

namespace CarRentalSystem.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}