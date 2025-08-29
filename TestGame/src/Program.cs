using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Oss.Sage;

Debug.WriteLine(
    """
    ===========================================================
    This is a temporary program and will eventually be removed.
    Used for testing purposes.
    ===========================================================
    """
);

using var loggerFactor = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(
#if DEBUG
        LogLevel.Debug
#else
        LogLevel.Information
#endif
    );

    builder.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = true;
        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff zzz ";
        options.UseUtcTimestamp = false;
    });
});

var logger = loggerFactor.CreateLogger<Program>();

using SageApp app = new(
    logger,
    options =>
    {
        options.AppName = typeof(Program).Assembly.GetName().Name;
        options.AppVersion = typeof(Program).Assembly.GetName().Version;
        options.AppCreator = "Oss.Sage Contributors";
        options.AppCopyright = "MIT Licensed";
        options.AppUrl = "https://github.com/vitimiti/Oss.Sage";
    }
);

app.Run();
