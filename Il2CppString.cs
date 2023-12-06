using System;
using Unity.Collections.LowLevel.Unsafe;

namespace IL2CPPUtility {


    public unsafe struct Il2CppStringHandle {
        public Il2CppString* Value;

        public Il2CppStringHandle(Il2CppString* value) {
            Value = value;
        }

        public Il2CppStringHandle(string str) {
            Value = (Il2CppString*) UnsafeUtility.As<string, IntPtr>(ref str);
        }

        public ref string CSString {
            get {
                var p = (IntPtr*) UnsafeUtility.AddressOf(ref this);
                return ref UnsafeUtility.As<IntPtr, string>(ref p[0]);
            }
        }
        public static implicit operator Il2CppStringHandle(string str) => new Il2CppStringHandle(str);
        public override string ToString() => CSString;
    }



    public unsafe struct Il2CppString {
        public Il2CppObject Object;
        public  int length;
        public  char* chars;
    }
}