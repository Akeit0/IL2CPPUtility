using System;
using System.Runtime.CompilerServices;
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

        public  object CSObject {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UnsafeUtility.As<Il2CppObjectHandle, object>(ref this);
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