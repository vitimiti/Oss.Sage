using JetBrains.Annotations;

namespace Oss.Sage.Options;

[PublicAPI]
public class SageAppOptions
{
    public string? AppName { get; set; }
    public Version? AppVersion { get; set; }
    public string? AppCreator { get; set; }
    public string? AppCopyright { get; set; }

    [UriString]
    public string? AppUrl { get; set; }

    public (int Width, int Height) WindowSize { get; set; } = (1280, 720);

    public override string ToString() =>
        $"{nameof(SageAppOptions)} {{ {nameof(AppName)} = {AppName}, {nameof(AppVersion)} = {AppVersion}), {nameof(AppCreator)} = {AppCreator}, {nameof(AppCopyright)} = {AppCopyright}, {nameof(AppUrl)} = {AppUrl}, {nameof(WindowSize)} = {WindowSize} }}";
}
