using Microsoft.Extensions.Configuration;
using System;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnConfiguration : ITurnConfiguration
    {
        public void AddConfiguration(ITurn turn)
        {
            if (turn.Directions.Have<Func<IConfiguration>>(out var configurationBuilder))
                turn.Directions.Add(configurationBuilder.Invoke());
        }
    }
}
