using CarRentalSystem.Db.Interfaces;
using CarRentalSystem.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalSystem.Db.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}