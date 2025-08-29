using Microsoft.Extensions.Logging;

namespace Oss.Sage.WwVegas.Ww3D2.Logging;

internal static partial class VegasAppLogging
{
    [LoggerMessage(LogLevel.Trace, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlTrace(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Debug, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlDebug(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Information, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlInfo(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Warning, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlWarning(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Error, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlError(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Critical, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlCritical(ILogger logger, string message, string category);

    [LoggerMessage(LogLevel.Warning, Message = "{Message} [SDL3:{Category}]")]
    public static partial void SdlInvalid(ILogger logger, string message, string category);

    [LoggerMessage(
        LogLevel.Information,
        Message = "Using SDL3 version {Version} (minimum required: {MinimumRequiredVersion})"
    )]
    public static partial void UsingSdl3Version(
        ILogger logger,
        string version,
        string minimumRequiredVersion
    );

    [LoggerMessage(LogLevel.Debug, Message = "Initializing SDL3...")]
    public static partial void InitializingSdl3(ILogger logger);

    [LoggerMessage(LogLevel.Debug, Message = "SDL3 initialized")]
    public static partial void Sdl3Initialized(ILogger logger);

    [LoggerMessage(LogLevel.Debug, Message = "Terminating SDL3...")]
    public static partial void TerminatingSdl3(ILogger logger);

    [LoggerMessage(LogLevel.Debug, Message = "SDL3 terminated")]
    public static partial void Sdl3Terminated(ILogger logger);
}
