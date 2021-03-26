using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnServices : ITurnServices
    {
        readonly Action<IConfiguration, IServiceCollection> _services;

        public TurnServices(Action<IConfiguration, IServiceCollection> services)
        {
            _services = services;
        }

        public void AddServices(ITurn turn)
        {            
            _services.Invoke(
                turn.Directions.Configuration(), 
                turn.Directions.ServiceCollection());            
        }
    }
}
