using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RightTurn.Extensions.Configuration
{
    public static class TurnConfigurationExtensions
    {
        public static ITurn WithServices(this ITurn turn, Action<IConfiguration, IServiceCollection> services)
        {
            turn.Directions.Add<ITurnServices>(new TurnServices(services));
            return turn;
        }

        public static ITurn WithConfiguration(this ITurn turn, Func<IConfiguration> builder)
        {
            turn.Directions.Add<ITurnConfiguration>(new TurnConfiguration(builder));
            return turn;
        }

        public static ITurn WithConfiguration(this ITurn turn, Func<IConfigurationBuilder, IConfigurationBuilder> builder) => turn.WithConfiguration(() =>
        {
            return builder
                .Invoke(new ConfigurationBuilder())
                .Build();
        });

        public static ITurn WithConfigurationFile(this ITurn turn, string name = "appsettings.json", bool optional = true) => WithConfiguration(turn, (builder) => builder            
            .AddJsonFile(name, optional));


        /// <summary>
        /// Bind configuration to a class and add the object as singleton.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="turn"></param>
        /// <param name="key">The key of the configuration section to bind.</param>
        /// <returns></returns>
        public static ITurn WithConfigurationSettings<T>(this ITurn turn, string key) where T : class, new()
        {
            turn.Directions.Add<TurnConfigurationBindings>().Add(key, (services) =>
            {
                var settings = new T();
                services.AddSingleton(settings);
                return settings;
            });

            return turn;
        }

        /// <summary>
        /// Bind configuration settings to a service class and add the service as singleton.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="turn"></param>
        /// <param name="key">The key of the configuration section to bind.</param>
        /// <returns></returns>
        public static ITurn WithConfigurationSettings<TService, TImplementation>(this ITurn turn, string key) where TService : class where TImplementation : class, TService, new()
        {
            turn.Directions.Add<TurnConfigurationBindings>().Add(key, (services) =>
            {
                var service = new TImplementation();
                services.AddSingleton<TService>(service);
                return service;
            });

            return turn;
        }
    }
}
