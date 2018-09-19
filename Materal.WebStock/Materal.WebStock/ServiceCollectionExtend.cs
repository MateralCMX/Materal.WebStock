using Materal.WebStock.CommandHandlers;
using Materal.WebStock.Commands;
using Materal.WebStock.EventHandlers;
using Materal.WebStock.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.WebStock
{
    public static class ServiceCollectionExtend
    {
        private static readonly object CommandLocker = new object();
        public static void AddCommandBus<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<ICommandBus<T>, CommandBusImpl<T>>();
            services.AddWebStockClientCommandHandlersSingleton<T>(assemblies);
        }
        public static void AddWebStockClientCommandHandlersSingleton<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Type> commandHandlerTypes = new List<Type>();
            foreach (var item in assemblies)
            {
                lock (CommandLocker)
                {
                    commandHandlerTypes.AddRange(GetCommandHandlerTypes<T>(item));
                }
            }
            ICommandHandlerHelper implementationInstance = new CommandHandlerHelperImpl(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }
        private static IEnumerable<Type> GetCommandHandlerTypes<T>(Assembly assembly)
        {
            var result = new List<Type>();
            var ihandlerType = typeof(ICommandHandler<T>);
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
        private static readonly object EventLocker = new object();
        public static void AddEventBus<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventBus<T>, EventBusImpl<T>>();
            services.AddWebStockClientEventHandlersSingleton<T>(assemblies);
        }
        public static void AddWebStockClientEventHandlersSingleton<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            List<Type> commandHandlerTypes = new List<Type>();
            foreach (var item in assemblies)
            {
                lock (EventLocker)
                {
                    commandHandlerTypes.AddRange(GetEventHandlerTypes<T>(item));
                }
            }
            IEventHandlerHelper implementationInstance = new EventHandlerHelperImpl(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }
        private static IEnumerable<Type> GetEventHandlerTypes<T>(Assembly assembly)
        {
            var result = new List<Type>();
            var ihandlerType = typeof(EventHandlers.IEventHandler<T>);
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
