using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Oss.WwVegas.Ww3D2.Logging;
using Oss.WwVegas.Ww3D2.NativeImports;
using Oss.WwVegas.Ww3D2.Options;

namespace Oss.WwVegas.Ww3D2;

[PublicAPI]
public class VegasApp : IDisposable
{
    [SuppressMessage(
        "csharpsquid",
        "S6669:Logger field or property name should comply with a naming convention",
        Justification = "Avoid naming conflicts with the instance logger."
    )]
    private static ILogger? s_logger;

    private readonly ILogger _logger;
    private readonly VegasAppOptions _vegasAppOptions = new();

    protected VegasApp(ILogger logger, Action<VegasAppOptions>? vegasAppOptions = null)
    {
        _logger = logger;
        SetStaticLogger(logger);

        vegasAppOptions?.Invoke(_vegasAppOptions);
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
        GenericLogging.Disposing(_logger, nameof(VegasApp));

        ReleaseUnmanagedResources();
        if (disposing)
        {
            // Nothing to dispose of here.
        }

        GenericLogging.Disposed(_logger, nameof(VegasApp));
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~VegasApp() => Dispose(disposing: false);

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

        var error = Sdl.SetAppMetadata(
            _vegasAppOptions.AppName,
            _vegasAppOptions.AppVersion,
            typeof(VegasApp).Assembly.GetName().Name?.ToLowerInvariant()
        );

        if (error is not null)
        {
            throw new InvalidOperationException("Unable to set app metadata", error);
        }

        error = Sdl.InitSubSystem(Sdl.InitFlags.Video | Sdl.InitFlags.Events);
        if (error is not null)
        {
            throw new InvalidOperationException("Unable to initialize SDL3", error);
        }

        VegasAppLogging.Sdl3Initialized(_logger);
    }

    protected void Initialize()
    {
        GenericLogging.Initializing(_logger, nameof(VegasApp));

        InitializeSdl();

        GenericLogging.Initialized(_logger, nameof(VegasApp));
    }

    public void Run()
    {
        Initialize();
    }
}
