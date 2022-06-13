using Api.Extensions;
using Business.Interfaces;
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



            return services;
        }
    }
}
