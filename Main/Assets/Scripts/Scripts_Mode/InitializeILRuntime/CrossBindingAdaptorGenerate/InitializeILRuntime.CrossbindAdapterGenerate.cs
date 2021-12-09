namespace ILRuntime.Moudle
{
    using ILRuntime.Moudle.CrossbindAdapter;

    /// <summary>
    /// 负责跨域继承适配器的注册(自动生成文件，请勿手动更改)
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册跨域继承适配器(发版前固定)
        /// </summary>
        private static void InitializeCrossbindAdapter(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdapter());
        }
        /// <summary>
        /// 注册跨域继承的特殊适配器
        /// </summary>
        private static void InitializeSpecialCrossbindAdapter(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            for (int i = 0; i < list_SpecialCrossbindAdapterTypes.Count; i++)
            {
                appdomain.RegisterCrossBindingAdaptor(System.Activator.CreateInstance(list_SpecialCrossbindAdapterTypes[i]) as Runtime.Enviorment.CrossBindingAdaptor);
            }
        }
    }
}

