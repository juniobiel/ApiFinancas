using Api.Extensions;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Services.Notifications;
using Data.Context;

namespace Api.Config
{
    public static class DependencyInjections
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<FinanceDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<INotificator, Notificator>();


            return services;
        }
    }
}
