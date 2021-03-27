using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace RightTurn.Extensions.Configuration
{
    internal class TurnConfigurationBindings : Dictionary<string, Func<IServiceCollection, object>>
    {
    
    }
}
