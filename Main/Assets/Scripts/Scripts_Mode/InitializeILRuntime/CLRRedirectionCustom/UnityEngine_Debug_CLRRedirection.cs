namespace ILRuntime.Moudle.CLRRedirection
{
    using System.Collections.Generic;
    using CLR.Method;
    using CLR.Utils;
    using Runtime.Intepreter;
    using Runtime.Stack;

    public static class UnityEngine_Debug_CLRRedirection
    {
        public unsafe static void RegisterILRuntimeCLRRedirection(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            foreach (var i in typeof(UnityEngine.Debug).GetMethods())
            {
                if (i.Name == "Log" && !i.IsGenericMethodDefinition)
                {
                    var param = i.GetParameters();
                    if (param[0].ParameterType == typeof(object))
                    {
                        appdomain.RegisterCLRMethodRedirection(i, Log_0);
                    }
                }
            }
        }


        unsafe static StackObject* Log_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

            object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var stacktrace = __domain.DebugService.GetStackTrace(__intp);

            UnityEngine.Debug.Log(message + "\n" + stacktrace);

            return __ret;
        }
    }
}