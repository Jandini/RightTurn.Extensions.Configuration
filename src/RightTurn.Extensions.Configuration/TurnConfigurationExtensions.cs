using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace RightTurn.Extensions.Configuration
{
    public static class TurnConfigurationExtensions
    {     
        public static ITurn WithConfiguration(this ITurn turn, Func<IConfiguration> builder)
        {
            turn.Directions.Add(builder);
            turn.Directions.Add<ITurnConfiguration>(new TurnConfiguration());
            return turn;
        }

        public static ITurn WithConfiguration(this ITurn turn, Func<IConfigurationBuilder, IConfigurationBuilder> builder) => turn.WithConfiguration(() =>
        {
            return builder
                .Invoke(new ConfigurationBuilder())
                .Build();
        });

        public static ITurn WithConfigurationFile(this ITurn turn, string name = "appsettings.json", bool optional = true) => WithConfiguration(turn, (builder) => builder
            .SetBasePath(Directory.GetCurrentDirectory())
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
            var settings = new T();
            var configuration = turn.Directions.Get<IConfiguration>();

            configuration.Bind(key, settings);
            turn.WithServices(services => services.AddSingleton(settings));

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
            var service = new TImplementation();
            var configuration = turn.Directions.Get<IConfiguration>();

            configuration.Bind(key, service);
            turn.WithServices(services => services.AddSingleton<TService>(service));

            return turn;
        }      
    }
}
