using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            Google_Protobuf_ProtoPreconditions_Binding.Register(app);
            Google_Protobuf_CodedOutputStream_Binding.Register(app);
            System_String_Binding.Register(app);
            Google_Protobuf_CodedInputStream_Binding.Register(app);
            Google_Protobuf_MessageParser_1_ILRuntime_Moudle_CrossBindingAdaptorCustom_ProtoIMessageAdapt_Binding_Adaptor_Binding.Register(app);
            Google_Protobuf_ByteString_Binding.Register(app);
            Google_Protobuf_Collections_RepeatedField_1_Int32_Binding.Register(app);
            Google_Protobuf_Collections_RepeatedField_1_String_Binding.Register(app);
            Google_Protobuf_Collections_RepeatedField_1_ByteString_Binding.Register(app);
            Google_Protobuf_Collections_RepeatedField_1_ILRuntime_Moudle_CrossBindingAdaptorCustom_ProtoIMessageAdapt_Binding_Adaptor_Binding.Register(app);
            Google_Protobuf_FieldCodec_Binding.Register(app);
            Google_Protobuf_Collections_MapField_2_String_Int32_Binding.Register(app);
            Google_Protobuf_Collections_MapField_2_String_Int32_Binding_Codec_Binding.Register(app);
            System_IO_MemoryStream_Binding.Register(app);
            Google_Protobuf_MessageExtensions_Binding.Register(app);
            System_IO_Stream_Binding.Register(app);
            System_Byte_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Text_StringBuilder_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Collections_IEnumerable_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Reflection_PropertyInfo_Binding.Register(app);
            System_Reflection_MethodBase_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncVoidMethodBuilder_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
