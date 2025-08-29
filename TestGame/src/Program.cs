using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TestGame;

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
#if DEBUG
    builder.SetMinimumLevel(LogLevel.Trace);
#else
    builder.SetMinimumLevel(LogLevel.Information);
#endif
    builder.AddConsole();
});

var logger = loggerFactor.CreateLogger<Program>();

using Game app = new(logger);
app.Run();
