using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Oss.Sage.NativeImports.CustomMarshallers;

[CustomMarshaller(
    typeof(ExternalException),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
internal static class ErrorReturnMarshaller
{
    public ref struct ManagedToUnmanagedOut
    {
        private ExternalException? _managed;

        public void FromUnmanaged(int unmanaged) =>
            _managed = unmanaged != 0 ? null : new ExternalException(Sdl.GetError());

        public ExternalException? ToManaged() => _managed;

        public void Free()
        {
            // Nothing to do.
        }
    }
}
