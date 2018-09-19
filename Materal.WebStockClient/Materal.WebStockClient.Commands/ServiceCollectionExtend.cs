using Materal.WebStockClient.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.WebStockClient.Commands
{
    public static class ServiceCollectionExtend
    {
        private static readonly object Locker = new object();
        public static void AddCommandBus<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IWebStockClientCommandBus<T>, WebStockClientCommandBus<T>>();
            services.AddWebStockClientCommandHandlersSingleton<T>(assemblies);
        }
        public static void AddWebStockClientCommandHandlersSingleton<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Type> commandHandlerTypes = new List<Type>();
            foreach (var item in assemblies)
            {
                lock (Locker)
                {
                    commandHandlerTypes.AddRange(GetCommandHandlerTypes<T>(item));
                }
            }
            IWebStockClientCommandHandlerHelper implementationInstance = new WebStockClientCommandHandlerHelper(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }

        private static IEnumerable<Type> GetCommandHandlerTypes<T>(Assembly assembly)
        {
            var result = new List<Type>();
            var ihandlerType = typeof(IWebStockClientCommandHandler<T>);
            var assemblyTypes = assembly.GetTypes();
            foreach (var item in assemblyTypes)
            {
                var interfaceTypes = item.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    if (interfaceType.GUID == ihandlerType.GUID)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }
}
