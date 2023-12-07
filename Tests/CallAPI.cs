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
        static Vector3 v3=new Vector3(1,2,3);
        
        public static string getV3() => v3.ToString()+"\n";
        public unsafe void Start() {
            var t = transform;
            V1 = t.position;
            Debug.Log(V1);
            try {
#if !UNITY_EDITOR
                #if !UNITY_WEBGL
            var iCallFunc=Il2CppApi.il2cpp_resolve_icall("UnityEngine.Transform::get_position_Injected(UnityEngine.Vector3&)").Value;
            var tPtr=(void*)UnsafeUtility.As<Transform, IntPtr>(ref t);
            var v2Ptr = UnsafeUtility.AddressOf(ref V2);
            BurstInterpreter.Invoke(iCallFunc,tPtr,v2Ptr);
            
            //((delegate*unmanaged[Cdecl]<Transform, ref Vector3, void>) iCallFunc)(t,ref V2);
            Debug.Log(V2);
#endif
           
            
            var type = this.GetType();
#if !UNITY_WEBGL
                var p= new    Il2CppMethodInfoHandle(type.GetMethod(nameof(getV3))).Value->methodPointer; 
            
             BurstInterpreter.InvokeRet(p.Value,out var sp);
              data =UnsafeUtility.As<IntPtr,string>(ref sp);
#endif
            using var str=new MarshaledString("V1");
            var klass = type.GetClassHandle();
            data += klass.Value->actualSize.ToString()+"\n";
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