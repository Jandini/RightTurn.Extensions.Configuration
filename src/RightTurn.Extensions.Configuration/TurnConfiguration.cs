using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnConfiguration : ITurnConfiguration
    {
        public static Dictionary<string, Func<IServiceCollection, object>> Bindings = new Dictionary<string, Func<IServiceCollection, object>>();

        public void AddConfiguration(ITurn turn)
        {
            if (turn.Directions.Have<Func<IConfiguration>>(out var configurationBuilder))
            {
                var configuration = configurationBuilder.Invoke();
                turn.Directions.Add(configuration);

                if (Bindings.Count > 0)
                {
                    var services = turn.Directions.ServiceCollection();
                    foreach (var bind in Bindings)
                        configuration.Bind(bind.Key, bind.Value.Invoke(services));
                }
            }
        }
    }
}
