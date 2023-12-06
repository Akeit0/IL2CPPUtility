using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace IL2CPPUtility.Tests {
    public class CallAPI : MonoBehaviour {
        public Vector3 V1;
        public Vector3 V2;
        string exception;
        string data="";
        public unsafe void Start() {
            var t = transform;
            V1 = t.position;
            Debug.Log(V1);
            try {
#if !UNITY_EDITOR
            var iCallFunc=(delegate*unmanaged[Cdecl]<Transform,ref Vector3,void>) Il2CppApi.il2cpp_resolve_icall("UnityEngine.Transform::get_position_Injected(UnityEngine.Vector3&)").Value;
            iCallFunc(t,ref V2);
            Debug.Log(V2);
         
            var type = this.GetType();
            using var str=new MarshaledString("V1");
            var klass = type.GetClassHandle();
            data = klass.Value->actualSize.ToString()+"\n";
            data = klass.Value->instance_size.ToString()+"\n";
            foreach (var f in klass.Fields) {
                if (f.name == str) {
                    data+=(f.offset+" "+(f.type.ClassHandle.Name.ToString()+ "V1 this!!")+"\n");
                }
                else {
                    data+=(f.offset+" "+(f.type.ClassHandle.Name.ToString()+f.name.ToString())+"\n");
                }
                    
            }


#endif
            }
            catch (Exception e) {
                exception = e.ToString();
                
                throw;
            }

        }

        void OnGUI() {
            
            
            GUI.Box (new Rect (0,0,100,50), V1.ToString());
            GUI.Box (new Rect (Screen.width - 100,0,100,50), V2.ToString());
            if(!string.IsNullOrEmpty(exception)) GUI.Box (new Rect (0,Screen.height - 50,100,50), exception);
            GUI.Box (new Rect (Screen.width - 500,Screen.height - 400,500,400), data);
        }
    }
}