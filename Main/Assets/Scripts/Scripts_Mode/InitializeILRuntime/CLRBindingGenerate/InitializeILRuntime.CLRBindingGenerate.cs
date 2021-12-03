namespace ILRuntime.Moudle
{
    /// <summary>
    /// 负责CLR绑定注册(自动生成文件，请勿手动更改)
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册CLR绑定(发版前固定)
        /// </summary>
        private static void InitializeCLRBinding(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
        }
    }
}

