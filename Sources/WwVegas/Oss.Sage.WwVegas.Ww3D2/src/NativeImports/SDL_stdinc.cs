using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Oss.Sage.WwVegas.Ww3D2.NativeImports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_strdup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* UnsafeStringDuplication(byte* str);

    [LibraryImport(LibraryName, EntryPoint = "SDL_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnsafeFree(void* memory);
}
