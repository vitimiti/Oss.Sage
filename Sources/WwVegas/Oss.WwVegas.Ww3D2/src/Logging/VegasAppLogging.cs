using Microsoft.Extensions.Logging;

namespace Oss.WwVegas.Ww3D2.Logging;

internal static partial class VegasAppLogging
{
    [LoggerMessage(LogLevel.Trace, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlTrace(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.Debug, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlDebug(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.Information, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlInfo(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.Warning, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlWarning(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.Error, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlError(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.Critical, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlCritical(ILogger logger, string category, string message);

    [LoggerMessage(LogLevel.None, Message = "[SDL3:{Category}] {Message}")]
    public static partial void SdlInvalid(ILogger logger, string category, string message);
}
