using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WebMarket.Products.Common.Cqrs.Abstractions;

namespace WebMarket.Products.Common.Helpers.Extensions;

public static class CqrsServiceCollectionExtensions
{
    private static readonly HashSet<Type> HandlerTypes = [typeof(IQueryHandler<,>), typeof(ICommandHandler<>), typeof(ICommandHandler<,>)];

    /// <summary>
    /// Регистрирует все обработчики запросов и команд сборки как scoped.
    /// </summary>
    public static IServiceCollection AddCqrsHandlers(this IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
                continue;

            foreach (var handlerType in type.GetInterfaces())
            {
                if (handlerType.IsGenericType && HandlerTypes.Contains(handlerType.GetGenericTypeDefinition()))
                    services.AddScoped(handlerType, type);
            }
        }

        return services;
    }
}
