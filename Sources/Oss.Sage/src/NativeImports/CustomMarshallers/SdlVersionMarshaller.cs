using System.Runtime.InteropServices.Marshalling;

namespace Oss.Sage.NativeImports.CustomMarshallers;

[CustomMarshaller(
    typeof(Version),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
internal static class SdlVersionMarshaller
{
    public ref struct ManagedToUnmanagedOut
    {
        private Version? _managed;

        public void FromUnmanaged(int unmanaged) =>
            _managed = new Version(
                unmanaged / 1_000_000,
                (unmanaged / 1_000) % 1_000,
                unmanaged % 1_000
            );

        public Version? ToManaged() => _managed;

        public void Free()
        {
            // Nothing to do.
        }
    }
}
