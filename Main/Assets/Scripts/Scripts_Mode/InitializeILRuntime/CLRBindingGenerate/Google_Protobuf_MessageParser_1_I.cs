using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Google_Protobuf_MessageParser_1_ILRuntime_Moudle_CrossBindingAdaptorCustom_ProtoIMessageAdapt_Binding_Adaptor_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>);

            args = new Type[]{typeof(System.Func<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }



        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Func<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor> @factory = (System.Func<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>)typeof(System.Func<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = new Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>(@factory);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
