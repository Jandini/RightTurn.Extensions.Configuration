using Microsoft.Extensions.Configuration;

namespace RightTurn.Extensions.Configuration
{
    public static class TurnDirectionsExtensions
    {
        public static IConfiguration Configuration(this ITurnDirections directions) => directions.TryGet<IConfiguration>();
    }
}
