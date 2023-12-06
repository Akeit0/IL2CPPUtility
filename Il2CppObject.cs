using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace IL2CPPUtility {
    public unsafe struct Il2CppObjectHandle {
        public Il2CppObject* Value;

        public Il2CppObjectHandle(Il2CppObject* value) {
            Value = value;
        }

        public Il2CppObjectHandle(object o) {
            Value = (Il2CppObject*) UnsafeUtility.As<object, IntPtr>(ref o);
        }

        public ref object CSObject {
            get {
                var p = (IntPtr*) UnsafeUtility.AddressOf(ref this);
                return ref UnsafeUtility.As<IntPtr, object>(ref p[0]);
            }
        }

        public override string ToString() => CSObject.ToString();
    }


    public unsafe struct Il2CppObject {
        public Il2CppClassHandle klass;
        MonitorData* monitor;
    }

    public struct MonitorData {
    }
}