using Microsoft.Extensions.Configuration;
using System;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnConfiguration : ITurnConfiguration
    {
        readonly Func<IConfiguration> _builder;

        public TurnConfiguration(Func<IConfiguration> builder)
        {
            _builder = builder;
        }

        public void AddConfiguration(ITurn turn)
        {
            var configuration = _builder.Invoke();
            turn.Directions.Add(configuration);

            if (turn.Directions.Have<TurnConfigurationBindings>(out var bindings)
                && bindings.Count > 0)
            {
                var services = turn.Directions.ServiceCollection();
                foreach (var binding in bindings)
                    configuration.Bind(binding.Key, binding.Value.Invoke(services));
            }
        }
    }
}
