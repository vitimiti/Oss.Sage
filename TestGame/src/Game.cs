using Microsoft.Extensions.Logging;
using Oss.Sage.WwVegas.Ww3D2;

namespace TestGame;

internal class Game(ILogger logger)
    : VegasApp(
        logger,
        options =>
        {
            options.AppName = typeof(Game).Assembly.GetName().Name;
            options.AppVersion = typeof(Game).Assembly.GetName().Version;
            options.AppCreator = "Oss.Sage Contributors";
            options.AppCopyright = "MIT Licensed";
            options.AppUrl = "https://github.com/vitimiti/Oss.Sage";
        }
    );
