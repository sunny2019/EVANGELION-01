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
        /// <param name="appDomain"></param>
        private static void InitializeDelegate(this ILRuntime.Runtime.Enviorment.AppDomain appDomain)
        {
            appDomain.DelegateManager.RegisterMethodDelegate<int>();
            appDomain.DelegateManager.RegisterMethodDelegate<float>();
            appDomain.DelegateManager.RegisterMethodDelegate<double>();
            appDomain.DelegateManager.RegisterMethodDelegate<bool>();
            appDomain.DelegateManager.RegisterMethodDelegate<string>();
            appDomain.DelegateManager.RegisterFunctionDelegate<int>();
            appDomain.DelegateManager.RegisterFunctionDelegate<float>();
            appDomain.DelegateManager.RegisterFunctionDelegate<double>();
            appDomain.DelegateManager.RegisterFunctionDelegate<bool>();
            appDomain.DelegateManager.RegisterFunctionDelegate<string>();
            appDomain.DelegateManager.RegisterFunctionDelegate<int, string>();

            
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<float>>((action) =>
            {
                return new UnityEngine.Events.UnityAction<float>((value) => { ((System.Action<float>) action)(value); });
            });
        }
    }
}