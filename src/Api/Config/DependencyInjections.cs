using Api.Config.Swagger;
using Api.Extensions;
using Business.Interfaces;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Services;
using Business.Services.Notifications;
using Data.Context;
using Data.Repository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Config
{
    public static class DependencyInjections
    {
        public static IServiceCollection ResolveDependencies( this IServiceCollection services )
        {
            services.AddScoped<FinanceDbContext>();

            services.AddScoped<INotificator, Notificator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockRepository, StockRepository>();

            services.AddScoped<IStockPurchaseService, StockPurchaseService>();
            services.AddScoped<IStockPurchaseRepository, StockPurchaseRepository>();

            return services;
        }
    }
}
