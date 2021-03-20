using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnConfiguration : ITurnConfiguration
    {
        public static Dictionary<string, Func<object>> Bindings = new Dictionary<string, Func<object>>();

        public void AddConfiguration(ITurn turn)
        {
            if (turn.Directions.Have<Func<IConfiguration>>(out var configurationBuilder))
            {
                var configuration = configurationBuilder.Invoke();
                turn.Directions.Add(configuration);

                foreach (var bind in Bindings)
                    configuration.Bind(bind.Key, bind.Value.Invoke());
            }
        }
    }
}
