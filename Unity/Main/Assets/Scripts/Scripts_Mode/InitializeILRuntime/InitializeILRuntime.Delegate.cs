namespace ILRuntime.Moudle
{
    /// <summary>
    /// 负责委托参数搭配注册，及适配转换器注册
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册委托参数及适配器(发版前固定)
        /// </summary>
        /// <param name="appdomain"></param>
        private static void InitializeDelegate(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterMethodDelegate<int>();
            appdomain.DelegateManager.RegisterMethodDelegate<float>();
            appdomain.DelegateManager.RegisterMethodDelegate<double>();
            appdomain.DelegateManager.RegisterMethodDelegate<bool>();
            appdomain.DelegateManager.RegisterMethodDelegate<string>();
            appdomain.DelegateManager.RegisterFunctionDelegate<int>();
            appdomain.DelegateManager.RegisterFunctionDelegate<float>();
            appdomain.DelegateManager.RegisterFunctionDelegate<double>();
            appdomain.DelegateManager.RegisterFunctionDelegate<bool>();
            appdomain.DelegateManager.RegisterFunctionDelegate<string>();
            appdomain.DelegateManager.RegisterFunctionDelegate<int, string>();
            appdomain.DelegateManager.RegisterFunctionDelegate<global::ILRuntime.Moudle.CrossBindingAdaptorCustom.ProtoIMessageAdapt.Adaptor>();
            appdomain.DelegateManager.RegisterFunctionDelegate<int, int>();
            
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<float>>((action) =>
            {
                return new UnityEngine.Events.UnityAction<float>((value) => { ((System.Action<float>) action)(value); });
            });
        }
    }
}