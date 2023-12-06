using System;
using System.Reflection;

namespace IL2CPPUtility {
    public unsafe struct Il2CppClassHandle {
        public Il2CppClass* Value;
        
        public Il2CppClassHandle(Type type) {
            this=Il2CppApi.il2cpp_class_from_type(type.GetTypeHandle());
        }
        public Il2CppClassHandle(Il2CppTypeHandle typeHandle) {
            this= Il2CppApi.il2cpp_class_from_type(typeHandle);
        }

        public void Init() {
            Il2CppApi.il2cpp_runtime_class_init(this);
        }
        
        public uint ActualSize => Value->actualSize;

        public object New() => Il2CppApi.il2cpp_object_new(this);

        public ReadOnlySpan<Il2CppClassHandle> ImplementedInterfaces {
            get {
                Init(); return new ReadOnlySpan<Il2CppClassHandle>(Value->implementedInterfaces, Value->interfaces_count);
            }
        }

        public ReadOnlySpan<Il2CppClassHandle> NestedTypes {
            get {  Init(); return new ReadOnlySpan<Il2CppClassHandle>(Value->nestedTypes, Value->nested_type_count); }
        }

        public ReadOnlySpan<Il2CppFieldInfoHandle.FieldInfo> Fields {
            get {   Init();return new ReadOnlySpan<Il2CppFieldInfoHandle.FieldInfo>(Value->fields.Value, Value->field_count); }
        }

        public ReadOnlySpan<Il2CppMethodInfoHandle> Methods {
            get {   Init();return new ReadOnlySpan<Il2CppMethodInfoHandle>(Value->methods, Value->method_count); }
        }
        
        public bool IsAssignableFrom(Il2CppClassHandle other) {
            return Il2CppApi.il2cpp_class_is_assignable_from(this, other);
        }
        
        public bool IsSubclassOf(Il2CppClassHandle other,bool checkInterfaces=true) {
            return Il2CppApi.il2cpp_class_is_subclass_of(this, other,checkInterfaces);
        }
        public NativeString Name=>Value->name;
        public NativeString Namespace=>Value->namespaze;
        
        public Il2CppClassHandle GetBaseClassHandle()=>Value->parent;
        public Type GetBaseCSType()=>Il2CppApi.il2cpp_type_get_object(new Il2CppTypeHandle(&Value->parent.Value->byval_arg)).CSType;
        public struct Il2CppClass {
             public Il2CppImageHandle image;
            public void* gc_desc;
            public NativeString name;
            public NativeString namespaze;
            public Il2CppTypeHandle.Il2CppType byval_arg;
            public Il2CppTypeHandle.Il2CppType this_arg;
            public Il2CppClassHandle element_class;
            public Il2CppClassHandle castClass;
            public Il2CppClassHandle declaringType;
            public Il2CppClassHandle parent;
            public Il2CppGenericClassHandle generic_class;
            public Il2CppMetadataTypeHandle typeMetadataHandle;
            public void* interopData;
            public Il2CppClassHandle klass;
            // The following fields need initialized before access. This can be done per field or as an aggregate via a call to Class::Init
            public Il2CppFieldInfoHandle fields;
            public Il2CppEventInfoHandle events;
            public Il2CppPropertyInfoHandle properties;
            public Il2CppMethodInfoHandle* methods;
            public Il2CppClassHandle* nestedTypes;
            public Il2CppClassHandle* implementedInterfaces;
            public Il2CppRuntimeInterfaceOffsetPairHandle interfaceOffsets;
            public void* static_fields;
            public void* rgctx_data;
            public Il2CppClassHandle* typeHierarchy;
            public void* unity_user_data;
            public uint initializationExceptionGCHandle;
            public uint cctor_started;
            public uint cctor_finished_or_no_cctor;
            public IntPtr cctor_thread;
            public Il2CppMetadataGenericContainerHandle genericContainerHandle;
            public uint instance_size;
            public uint stack_slot_size;
            public uint actualSize;
            public uint element_size;
            public int native_size;
            public uint static_fields_size;
            public uint thread_static_fields_size;
            public int thread_static_fields_offset;
            public TypeAttributes flags;
            public uint token;
            public ushort method_count;
            public ushort property_count;
            public ushort field_count;
            public ushort event_count;
            public ushort nested_type_count;
            public ushort vtable_count;
            public ushort interfaces_count;
            public ushort interface_offsets_count;
            public byte typeHierarchyDepth;
            public byte genericRecursionDepth;
            public byte rank;
            public byte minimumAlignment;
            public byte packingSize;
            public Bitfield0 _bitfield0;
            public Bitfield1 _bitfield1;
            public enum Bitfield0 : byte
            {
                BIT_initialized_and_no_error = 0,
                initialized_and_no_error = (1 << BIT_initialized_and_no_error),
                BIT_initialized = 1,
                initialized = (1 << BIT_initialized),
                BIT_enumtype = 2,
                enumtype = (1 << BIT_enumtype),
                BIT_nullabletype = 3,
                nullabletype = (1 << BIT_nullabletype),
                BIT_is_generic = 4,
                is_generic = (1 << BIT_is_generic),
                BIT_has_references = 5,
                has_references = (1 << BIT_has_references),
                BIT_init_pending = 6,
                init_pending = (1 << BIT_init_pending),
                BIT_size_init_pending = 7,
                size_init_pending = (1 << BIT_size_init_pending),
            }

            public enum Bitfield1 : byte
            {
                BIT_size_inited = 0,
                size_inited = (1 << BIT_size_inited),
                BIT_has_finalize = 1,
                has_finalize = (1 << BIT_has_finalize),
                BIT_has_cctor = 2,
                has_cctor = (1 << BIT_has_cctor),
                BIT_is_blittable = 3,
                is_blittable = (1 << BIT_is_blittable),
                BIT_is_import_or_windows_runtime = 4,
                is_import_or_windows_runtime = (1 << BIT_is_import_or_windows_runtime),
                BIT_is_vtable_initialized = 5,
                is_vtable_initialized = (1 << BIT_is_vtable_initialized),
                BIT_is_byref_like = 6,
                is_byref_like = (1 << BIT_is_byref_like),
            }
        }
    }
}