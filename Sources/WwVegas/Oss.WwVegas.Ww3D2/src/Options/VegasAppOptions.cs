using JetBrains.Annotations;

namespace Oss.WwVegas.Ww3D2.Options;

[PublicAPI]
public class VegasAppOptions
{
    public string? AppName { get; set; }
    public string? AppVersion { get; set; }

    public override string ToString() =>
        $"{nameof(VegasAppOptions)} {{ {nameof(AppName)} = {AppName}, {nameof(AppVersion)} = {AppVersion})}}";
}
