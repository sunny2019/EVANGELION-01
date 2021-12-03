namespace ILRuntime.Moudle
{
    using UnityEngine;
    using ILRuntime.Moudle.CLRRedirection;
    using ILRuntime.Moudle.ValueTypeBinder;
    /// <summary>
    /// 负责委托参数搭配注册，及适配转换器注册
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册CLR重定向(发版前固定)
        /// </summary>
        /// <param name="appdomain"></param>
        private static void InitializeCLRRedirection(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            UnityEngine_Debug_CLRRedirection.RegisterILRuntimeCLRRedirection(appdomain);
            UnityEngine_GameObject_CLRRedirection.RegisterILRuntimeCLRRedirection(appdomain);
            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
        }


        /// <summary>
        /// 注册CLR重定向(发版前固定)
        /// </summary>
        /// <param name="appdomain"></param>
        private static void InitializeValueTypeBinder(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
            appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
            appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        }
    }
}