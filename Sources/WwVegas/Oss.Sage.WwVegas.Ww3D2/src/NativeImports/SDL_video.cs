using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Oss.Sage.WwVegas.Ww3D2.NativeImports.CustomMarshallers;

namespace Oss.Sage.WwVegas.Ww3D2.NativeImports;

internal static partial class Sdl
{
    public record struct WindowFlags(ulong Value)
    {
        public static WindowFlags Fullscreen => new(0x00_00_00_00_00_00_00_01UL);
        public static WindowFlags Resizable => new(0x00_00_00_00_00_00_00_20UL);

        public static WindowFlags operator |(WindowFlags left, WindowFlags right) =>
            new(left.Value | right.Value);

        public static WindowFlags operator &(WindowFlags left, WindowFlags right) =>
            new(left.Value & right.Value);

        public static WindowFlags operator ^(WindowFlags left, WindowFlags right) =>
            new(left.Value ^ right.Value);

        public static WindowFlags operator ~(WindowFlags value) => new(~value.Value);
    }

    [NativeMarshalling(typeof(SafeHandleMarshaller<Window>))]
    public sealed class Window : SafeHandle
    {
        public override bool IsInvalid => handle == nint.Zero;

        public Window()
            : base(invalidHandleValue: nint.Zero, ownsHandle: true) => SetHandle(nint.Zero);

        public static Window Create(string title, int width, int height, WindowFlags flags)
        {
            var window = CreateWindow(title, width, height, flags);
            return window.IsInvalid ? throw new ExternalException(GetError()) : window;
        }

        protected override bool ReleaseHandle()
        {
            DestroyWindow(handle);
            SetHandle(nint.Zero);
            SetHandleAsInvalid();
            return true;
        }
    }

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_CreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window CreateWindow(
        string title,
        int width,
        int height,
        WindowFlags flags
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void DestroyWindow(nint window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ErrorReturnMarshaller))]
    public static partial ExternalException? DisableScreenSaver();
}
