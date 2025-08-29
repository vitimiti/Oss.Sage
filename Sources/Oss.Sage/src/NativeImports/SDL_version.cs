using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Oss.Sage.NativeImports.CustomMarshallers;

namespace Oss.Sage.NativeImports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlVersionMarshaller))]
    public static partial Version GetVersion();
}
