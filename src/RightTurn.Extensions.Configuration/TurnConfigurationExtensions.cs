using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace RightTurn.Extensions.Configuration
{
    public static class TurnConfigurationExtensions
    {
        public static ITurn WithConfiguration(this ITurn turn)
        {
            turn.Directions.Add<ITurnConfiguration>(new TurnConfiguration());
            return turn;
        }

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
    }
}
