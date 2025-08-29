using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Oss.Sage.WwVegas.Ww3D2.NativeImports.CustomMarshallers;

namespace Oss.Sage.WwVegas.Ww3D2.NativeImports;

internal static partial class Sdl
{
    public record struct InitFlags(uint Value)
    {
        public static InitFlags Video => new(0x00000020U);
        public static InitFlags Events => new(0x00004000U);

        public static InitFlags operator |(InitFlags left, InitFlags right) =>
            new(left.Value | right.Value);

        public static InitFlags operator &(InitFlags left, InitFlags right) =>
            new(left.Value & right.Value);

        public static InitFlags operator ^(InitFlags left, InitFlags right) =>
            new(left.Value ^ right.Value);

        public static InitFlags operator ~(InitFlags value) => new(~value.Value);
    }

    [LibraryImport(LibraryName, EntryPoint = "SDL_InitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ErrorReturnMarshaller))]
    public static partial ExternalException? InitSubSystem(InitFlags flags);

    [LibraryImport(LibraryName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_SetAppMetadata",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ErrorReturnMarshaller))]
    public static partial ExternalException? SetAppMetadata(
        string? appName,
        string? appVersion,
        string? appIdentifier
    );
}
