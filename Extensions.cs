using System.Reflection;
using System;
namespace IL2CPPUtility {
    public static class Extensions {
        public static Il2CppFieldInfoHandle GetHandle(this FieldInfo fieldInfo) {
            return new Il2CppFieldInfoHandle(fieldInfo);
        }
        public static Il2CppClassHandle GetClassHandle(this Type type) {
            return new Il2CppClassHandle(type);
        }
        public static Il2CppTypeHandle GetTypeHandle(this Type type) {
            return new Il2CppTypeHandle(type);
        }
        public static unsafe Il2CppClassHandle GetClassHandleFromObject(this object obj) {
            return new Il2CppObjectHandle(obj).Value->klass;
        }
        public static unsafe Il2CppTypeHandle GetTypeHandleFromObject(this object obj) {
            return new Il2CppTypeHandle(&(new Il2CppObjectHandle(obj).Value->klass.Value->this_arg));
        }
    }
}