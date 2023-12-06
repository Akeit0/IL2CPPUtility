
namespace IL2CPPUtility {
    public unsafe struct Il2CppFieldInfoHandle {
        public FieldInfo* Value;
        
        public Il2CppFieldInfoHandle(System.Reflection.FieldInfo fieldInfo) {
            Value =(FieldInfo*) fieldInfo.FieldHandle.Value;
        }
        
        public int Offset=>Value->offset;
        
        public bool IsThreadStatic=>Value->offset==-1;
        
        public Il2CppClassHandle Parent=>Value->parent;
        public Il2CppTypeHandle Type=>Value->type;
        public struct FieldInfo {
            public  NativeString name;
            public Il2CppTypeHandle type;
            public Il2CppClassHandle parent;
            public int offset;
            public uint token;
        }
    }
}