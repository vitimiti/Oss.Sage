using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using JetBrains.Annotations;

namespace Oss.WwVegas.Ww3D2.NativeImports;

internal static unsafe partial class Sdl
{
    public enum LogCategory
    {
        [UsedImplicitly]
        Application,

        [UsedImplicitly]
        Error,

        [UsedImplicitly]
        Assert,

        [UsedImplicitly]
        System,

        [UsedImplicitly]
        Audio,

        [UsedImplicitly]
        Video,

        [UsedImplicitly]
        Render,

        [UsedImplicitly]
        Input,

        [UsedImplicitly]
        Test,

        [UsedImplicitly]
        Gpu,
    }

    public enum LogPriority
    {
        [UsedImplicitly]
        Invalid,
        Trace,
        Verbose,
        Debug,
        Info,
        Warn,
        Error,
        Critical,
    }

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetLogPriorities")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetLogPriorities(LogPriority priority);

    public delegate void LogOutputFunction(
        LogCategory category,
        LogPriority priority,
        string message
    );

    private static readonly delegate* unmanaged[Cdecl]<
        nint,
        int,
        LogPriority,
        byte*,
        void> LogOutputFunctionPtr = &LogOutputFunctionImpl;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void LogOutputFunctionImpl(
        nint userdata,
        int category,
        LogPriority priority,
        byte* message
    )
    {
        var callback = GCHandle.FromIntPtr(userdata).Target as LogOutputFunction;
        callback?.Invoke(
            (LogCategory)category,
            priority,
            Utf8StringMarshaller.ConvertToManaged(message) ?? string.Empty
        );
    }

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetLogOutputFunction")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void UnsafeSetLogOutputFunction(
        delegate* unmanaged[Cdecl]<nint, int, LogPriority, byte*, void> callback,
        nint userdata
    );

    public static GCHandle LogOutputFunctionHandle { get; private set; }

    public static void SetLogOutputFunction(LogOutputFunction callback)
    {
        LogOutputFunctionHandle = GCHandle.Alloc(callback);
        UnsafeSetLogOutputFunction(
            LogOutputFunctionPtr,
            LogOutputFunctionHandle.IsAllocated
                ? GCHandle.ToIntPtr(LogOutputFunctionHandle)
                : nint.Zero
        );
    }
}
