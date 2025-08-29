using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Oss.Sage.WwVegas.Ww3D2.NativeImports.CustomMarshallers;

namespace Oss.Sage.WwVegas.Ww3D2.NativeImports;

internal static partial class Sdl
{
    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetError",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedStringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetError();
}
