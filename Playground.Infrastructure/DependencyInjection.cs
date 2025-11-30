using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Interfaces;
using Playground.Domain.Interfaces;
using Playground.Infrastructure.Services;

namespace Playground.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}