using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Interfaces;
using Playground.Infrastructure.Services;
using Polly;

namespace Playground.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri("https://692d1c0ce5f67cd80a4a21eb.mockapi.io/api/");
        })
        .AddTransientHttpErrorPolicy(p =>
            p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(200 * retryAttempt)))
        .AddTransientHttpErrorPolicy(p =>
            p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddScoped<IOrderService, OrderService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<ICurrencyService, CurrencyService>();

        return services;
    }
}