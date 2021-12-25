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
    unsafe class Google_Protobuf_FieldCodec_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Google.Protobuf.FieldCodec);
            args = new Type[]{typeof(System.UInt32)};
            method = type.GetMethod("ForInt32", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ForInt32_0);
            args = new Type[]{typeof(System.UInt32)};
            method = type.GetMethod("ForString", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ForString_1);
            args = new Type[]{typeof(System.UInt32)};
            method = type.GetMethod("ForBytes", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ForBytes_2);
            Dictionary<string, List<MethodInfo>> genericMethods = new Dictionary<string, List<MethodInfo>>();
            List<MethodInfo> lst = null;                    
            foreach(var m in type.GetMethods())
            {
                if(m.IsGenericMethodDefinition)
                {
                    if (!genericMethods.TryGetValue(m.Name, out lst))
                    {
                        lst = new List<MethodInfo>();
                        genericMethods[m.Name] = lst;
                    }
                    lst.Add(m);
                }
            }
            args = new Type[]{typeof(System.Int32)};
            if (genericMethods.TryGetValue("ForEnum", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(Google.Protobuf.FieldCodec<System.Int32>), typeof(System.UInt32), typeof(System.Func<System.Int32, System.Int32>), typeof(System.Func<System.Int32, System.Int32>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ForEnum_3);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor)};
            if (genericMethods.TryGetValue("ForMessage", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(Google.Protobuf.FieldCodec<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>), typeof(System.UInt32), typeof(Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ForMessage_4);

                        break;
                    }
                }
            }


        }


        static StackObject* ForInt32_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt32 @tag = (uint)ptr_of_this_method->Value;


            var result_of_this_method = Google.Protobuf.FieldCodec.ForInt32(@tag);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* ForString_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt32 @tag = (uint)ptr_of_this_method->Value;


            var result_of_this_method = Google.Protobuf.FieldCodec.ForString(@tag);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* ForBytes_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt32 @tag = (uint)ptr_of_this_method->Value;


            var result_of_this_method = Google.Protobuf.FieldCodec.ForBytes(@tag);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* ForEnum_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Func<System.Int32, System.Int32> @fromInt32 = (System.Func<System.Int32, System.Int32>)typeof(System.Func<System.Int32, System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Func<System.Int32, System.Int32> @toInt32 = (System.Func<System.Int32, System.Int32>)typeof(System.Func<System.Int32, System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.UInt32 @tag = (uint)ptr_of_this_method->Value;


            var result_of_this_method = Google.Protobuf.FieldCodec.ForEnum<System.Int32>(@tag, @toInt32, @fromInt32);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* ForMessage_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor> @parser = (Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>)typeof(Google.Protobuf.MessageParser<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.UInt32 @tag = (uint)ptr_of_this_method->Value;


            var result_of_this_method = Google.Protobuf.FieldCodec.ForMessage<ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>(@tag, @parser);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
