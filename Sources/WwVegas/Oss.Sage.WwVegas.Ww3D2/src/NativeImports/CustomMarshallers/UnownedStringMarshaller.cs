using System.Runtime.InteropServices.Marshalling;

namespace Oss.Sage.WwVegas.Ww3D2.NativeImports.CustomMarshallers;

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
internal static unsafe class UnownedStringMarshaller
{
    public ref struct ManagedToUnmanagedOut
    {
        private byte* _unmanaged;
        private string? _managed;

        public void FromUnmanaged(byte* unmanaged)
        {
            if (unmanaged is null)
            {
                _unmanaged = null;
                _managed = null;
                return;
            }

            _unmanaged = Sdl.UnsafeStringDuplication(unmanaged);
            _managed = Utf8StringMarshaller.ConvertToManaged(unmanaged);
        }

        public string? ToManaged() => _managed;

        public void Free()
        {
            if (_unmanaged is not null)
            {
                Sdl.UnsafeFree(_unmanaged);
            }
        }
    }
}
