using Microsoft.Extensions.Logging;

namespace Oss.WwVegas.Ww3D2.Logging;

internal static partial class GenericLogging
{
    [LoggerMessage(LogLevel.Debug, Message = "{ClassName} initializing...")]
    public static partial void Initializing(ILogger logger, string className);

    [LoggerMessage(LogLevel.Debug, Message = "{ClassName} initialized")]
    public static partial void Initialized(ILogger logger, string className);

    [LoggerMessage(LogLevel.Debug, Message = "{ClassName} disposing...")]
    public static partial void Disposing(ILogger logger, string className);

    [LoggerMessage(LogLevel.Debug, Message = "{ClassName} disposed")]
    public static partial void Disposed(ILogger logger, string className);
}
