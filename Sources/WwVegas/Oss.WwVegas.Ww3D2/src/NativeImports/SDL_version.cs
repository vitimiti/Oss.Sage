using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Oss.WwVegas.Ww3D2.NativeImports.CustomMarshallers;

namespace Oss.WwVegas.Ww3D2.NativeImports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlVersionMarshaller))]
    public static partial Version GetVersion();
}
