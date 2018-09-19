using Materal.WebStock.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.WebStock.Events
{
    public static class ServiceCollectionExtend
    {
        private static readonly object Locker = new object();
        public static void AddEventBus<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IWebStockClientEventBus<T>, WebStockClientEventBus<T>>();
            services.AddWebStockClientEventHandlersSingleton<T>(assemblies);
        }
        public static void AddWebStockClientEventHandlersSingleton<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Type> commandHandlerTypes = new List<Type>();
            foreach (var item in assemblies)
            {
                lock (Locker)
                {
                    commandHandlerTypes.AddRange(GetEventHandlerTypes<T>(item));
                }
            }
            IWebStockClientEventHandlerHelper implementationInstance = new WebStockClientEventHandlerHelper(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }

        private static IEnumerable<Type> GetEventHandlerTypes<T>(Assembly assembly)
        {
            var result = new List<Type>();
            var ihandlerType = typeof(IWebStockClientEventHandler<T>);
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
