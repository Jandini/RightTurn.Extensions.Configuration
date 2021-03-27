using Microsoft.Extensions.Configuration;
using RightTurn.Exceptions;

namespace RightTurn.Extensions.Configuration
{
    public static class TurnDirectionsExtensions
    {
        public static IConfiguration Configuration(this ITurnDirections directions)
        {
            if (!directions.Have<IConfiguration>(out var configuration))
                throw new TurnConfigurationRequiredException();

            return configuration;
        } 
    }
}
