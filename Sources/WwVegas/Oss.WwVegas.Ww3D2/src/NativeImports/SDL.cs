using System.Runtime.InteropServices;

namespace Oss.WwVegas.Ww3D2.NativeImports;

internal static partial class Sdl
{
    private const string LibraryName = "SDL3";

    private static KeyValuePair<string, bool>[] LibraryNames =>
        [
            // Add more as necessary. The preferred order is most common to least common desktops
            new("SDL3.dll", OperatingSystem.IsWindows()),
            new("libSDL3.so.0.2.16", OperatingSystem.IsLinux()),
        ];

    static Sdl() =>
        NativeLibrary.SetDllImportResolver(
            typeof(Sdl).Assembly,
            (name, assembly, path) =>
                NativeLibrary.Load(
                    LibraryNames.FirstOrDefault(pair => pair.Value).Key ?? name,
                    assembly,
                    path
                )
        );
}
