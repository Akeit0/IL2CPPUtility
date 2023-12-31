﻿
using System.Reflection;
using Unity.Collections.LowLevel.Unsafe;

namespace IL2CPPUtility {
    public unsafe struct Il2CppFieldInfoHandle {
        public FieldInfo* Value;
        
        public Il2CppFieldInfoHandle(System.Reflection.FieldInfo fieldInfo) {
            Value =(FieldInfo*) fieldInfo.FieldHandle.Value;
        }
        
        public int Offset=>Value->offset;
        
        public bool IsThreadStatic=>Value->offset==-1;
        
        public bool IsStatic=> ((FieldAttributes) (Value->type.Value->attrs) & FieldAttributes.Static)!=0;
        
        public bool IsPublic=> ((FieldAttributes) (Value->type.Value->attrs) & FieldAttributes.Public)!=0;
        public void SetValue(object obj,void* value) {
           Il2CppApi.il2cpp_field_set_value(new Il2CppObjectHandle(obj),this,value);
        }
       
        public void SetStaticValue(void* value) {
            Il2CppApi.il2cpp_field_static_set_value(this,value);
        }
        public void GetValue(object obj,void* value) {
             Il2CppApi.il2cpp_field_get_value(new Il2CppObjectHandle(obj),this,value);
        }
        public void GetStaticValue(void* value) {
            Il2CppApi.il2cpp_field_static_get_value(this,value);
        }
        public Il2CppClassHandle Parent=>Value->parent;
        public Il2CppTypeHandle Type=>Value->type;
        public struct FieldInfo {
            public  NativeString name;
            public Il2CppTypeHandle type;
            public Il2CppClassHandle parent;
            public int offset;
            public uint token;
            public FieldAttributes Attributes =>(FieldAttributes) type.Value->attrs ;
        }
    }
}