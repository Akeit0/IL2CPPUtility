using System;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace IL2CPPUtility {
    public unsafe struct MarshaledString:IDisposable {
        public override bool Equals(object obj) {
            return obj is MarshaledString other && Equals(other);
        }

        public override int GetHashCode() {
            return unchecked((int) (long) Chars);
        }

        public byte* Chars;
        public MarshaledString(string str) {
            Chars=(byte*)Marshal.StringToHGlobalAnsi(str);
        }
        public static bool operator ==(MarshaledString a, MarshaledString b) => a.Equals(b);
        public static bool operator !=(MarshaledString a, MarshaledString b) => !a.Equals(b);
        public static bool operator ==(MarshaledString a, NativeString b) => a.Equals(b);
        public static bool operator !=(MarshaledString a, NativeString b) => !a.Equals(b);
        
        public bool Equals(NativeString other) => NativeString.AnsiEquals(Chars,other.Chars);
        public bool Equals(MarshaledString other) => NativeString.AnsiEquals(Chars,other.Chars);
        public void Dispose() {
            if(Chars==null) return;
            Marshal.FreeHGlobal((IntPtr)Chars);
            Chars = null;
        }
    }
    
    public  unsafe struct NativeString {
        public override bool Equals(object obj) {
            return obj is NativeString other && Equals(other);
        }

        public override int GetHashCode() {
            return unchecked((int) (long) Chars);
        }

        public byte* Chars;
        public NativeString(IntPtr str) {
            Chars=(byte*)str;
        }
        public NativeString(byte* str) {
            Chars=str;
        }
        public override string ToString() {
            if(Chars==null||Chars[0]==0) return "";
            return Marshal.PtrToStringAnsi((IntPtr) Chars);
        }
        
        public static explicit operator string(NativeString str) => str.ToString();
        public static bool operator ==(NativeString a, NativeString b) => a.Equals(b);
        public static bool operator !=(NativeString a, NativeString b) => !a.Equals(b);
        public static bool operator ==(NativeString a, MarshaledString b) => a.Equals(b);
        public static bool operator !=(NativeString a, MarshaledString b) => !a.Equals(b);
        
        public static bool AnsiEquals(byte* a, byte* b) {
            
            if(a==b) return true;
            if(a==null||b==null) return false;
            while ((*a & *b)!=0) {
                if(*a++!=*b++)return false;
            }
            return true;
        }
        public bool Equals(NativeString other) => AnsiEquals(Chars,other.Chars);
        public bool Equals(MarshaledString other) => AnsiEquals(Chars,other.Chars);
        
        
    }
}