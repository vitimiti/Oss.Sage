using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Oss.Sage.Logging;
using Oss.Sage.NativeImports;
using Oss.Sage.Options;

namespace Oss.Sage;

[PublicAPI]
public class SageApp : IDisposable
{
    [SuppressMessage(
        "csharpsquid",
        "S6669:Logger field or property name should comply with a naming convention",
        Justification = "Avoid naming conflicts with the instance logger."
    )]
    private static ILogger? s_logger;

    private readonly ILogger _logger;
    private readonly SageAppOptions _sageAppOptions = new();

    private Sdl.Window? _window;

    public SageApp(ILogger logger, Action<SageAppOptions>? sageAppOptions = null)
    {
        _logger = logger;
        SetStaticLogger(logger);

        sageAppOptions?.Invoke(_sageAppOptions);
    }

    private static void SetStaticLogger(ILogger logger) => s_logger = logger;

    private void ReleaseUnmanagedResources()
    {
        VegasAppLogging.TerminatingSdl3(_logger);

        Sdl.Quit();
        if (Sdl.LogOutputFunctionHandle.IsAllocated)
        {
            Sdl.LogOutputFunctionHandle.Free();
        }

        VegasAppLogging.Sdl3Terminated(_logger);
    }

    protected virtual void Dispose(bool disposing)
    {
        GenericLogging.Disposing(_logger, nameof(SageApp));

        ReleaseUnmanagedResources();
        if (disposing)
        {
            _window?.Dispose();
        }

        GenericLogging.Disposed(_logger, nameof(SageApp));
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~SageApp() => Dispose(disposing: false);

    private static void SdlLoggingFunction(
        Sdl.LogCategory category,
        Sdl.LogPriority priority,
        string message
    )
    {
        ArgumentNullException.ThrowIfNull(s_logger);

        switch (priority)
        {
            case Sdl.LogPriority.Trace or Sdl.LogPriority.Verbose:
                VegasAppLogging.SdlTrace(s_logger, message, category.ToString());
                break;
            case Sdl.LogPriority.Debug:
                VegasAppLogging.SdlDebug(s_logger, message, category.ToString());
                break;
            case Sdl.LogPriority.Info:
                VegasAppLogging.SdlInfo(s_logger, message, category.ToString());
                break;
            case Sdl.LogPriority.Warn:
                VegasAppLogging.SdlWarning(s_logger, message, category.ToString());
                break;
            case Sdl.LogPriority.Error:
                VegasAppLogging.SdlError(s_logger, message, category.ToString());
                break;
            case Sdl.LogPriority.Critical:
                VegasAppLogging.SdlCritical(s_logger, message, category.ToString());
                break;
            default:
                VegasAppLogging.SdlInvalid(s_logger, message, category.ToString());
                break;
        }
    }

    private void InitializeSdlMetadata()
    {
        VegasAppLogging.InitializingSdl3Metadata(_logger);

        var error = Sdl.SetAppMetadata(
            _sageAppOptions.AppName,
            _sageAppOptions.AppVersion?.ToString(),
            typeof(SageApp).Assembly.GetName().Name?.ToLowerInvariant()
        );

        if (error is not null)
        {
            throw new InvalidOperationException("Unable to set app metadata", error);
        }

        error = Sdl.SetAppMetadataProperty(
            Sdl.PropertyAppMetadataCreatorString,
            _sageAppOptions.AppCreator
        );

        if (error is not null)
        {
            throw new InvalidOperationException(
                "Unable to set app metadata creator property",
                error
            );
        }

        error = Sdl.SetAppMetadataProperty(
            Sdl.PropertyAppMetadataCopyrightString,
            _sageAppOptions.AppCopyright
        );

        if (error is not null)
        {
            throw new InvalidOperationException(
                "Unable to set app metadata copyright property",
                error
            );
        }

        error = Sdl.SetAppMetadataProperty(
            Sdl.PropertyAppMetadataUrlString,
            _sageAppOptions.AppUrl
        );

        if (error is not null)
        {
            throw new InvalidOperationException("Unable to set app metadata url property", error);
        }

        error = Sdl.SetAppMetadataProperty(Sdl.PropertyAppMetadataTypeString, "Game");
        if (error is not null)
        {
            throw new InvalidOperationException("Unable to set app metadata type property", error);
        }

        VegasAppLogging.Sdl3MetadataInitialized(_logger);
    }

    private void LogAppMetadata()
    {
        VegasAppLogging.AppCreator(
            _logger,
            Sdl.GetAppMetadataProperty(Sdl.PropertyAppMetadataCreatorString)
        );

        VegasAppLogging.AppCopyright(
            _logger,
            Sdl.GetAppMetadataProperty(Sdl.PropertyAppMetadataCopyrightString)
        );

        VegasAppLogging.AppUrl(
            _logger,
            Sdl.GetAppMetadataProperty(Sdl.PropertyAppMetadataUrlString)
        );
    }

    private void InitializeSdl()
    {
        VegasAppLogging.InitializingSdl3(_logger);

        Sdl.SetLogPriorities(
#if DEBUG
            Sdl.LogPriority.Debug
#else
            Sdl.LogPriority.Info
#endif
        );

        Sdl.SetLogOutputFunction(SdlLoggingFunction);

        Version requiredSdlVersion = new(3, 2, 16);
        if (Sdl.GetVersion() < requiredSdlVersion)
        {
            throw new InvalidOperationException(
                $"SDL3 version {requiredSdlVersion} or higher is required."
            );
        }

        VegasAppLogging.UsingSdl3Version(
            _logger,
            Sdl.GetVersion().ToString(),
            requiredSdlVersion.ToString()
        );

        InitializeSdlMetadata();

        var error = Sdl.InitSubSystem(Sdl.InitFlags.Video | Sdl.InitFlags.Events);
        if (error is not null)
        {
            throw new InvalidOperationException("Unable to initialize SDL3", error);
        }

        LogAppMetadata();

        try
        {
            _window = Sdl.Window.Create(
                _sageAppOptions.AppName ?? "Oss.Sage",
                _sageAppOptions.WindowSize.Width,
                _sageAppOptions.WindowSize.Height,
                Sdl.WindowFlags.Fullscreen | Sdl.WindowFlags.Resizable
            );
        }
        catch (ExternalException exception)
        {
            throw new InvalidOperationException("Unable to create SDL3 window", exception);
        }

        error = Sdl.DisableScreenSaver();
        if (error is not null)
        {
            throw new InvalidOperationException("Unable to disable screen saver", error);
        }

        VegasAppLogging.Sdl3Initialized(_logger);
    }

    protected void Initialize()
    {
        GenericLogging.Initializing(_logger, nameof(SageApp));

        InitializeSdl();

        GenericLogging.Initialized(_logger, nameof(SageApp));
    }

    public void Run()
    {
        Initialize();
    }
}
