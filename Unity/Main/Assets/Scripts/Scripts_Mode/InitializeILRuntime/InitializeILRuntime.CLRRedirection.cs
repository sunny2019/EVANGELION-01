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
        /// <param name="appDomain"></param>
        private static void InitializeCLRRedirection(this ILRuntime.Runtime.Enviorment.AppDomain appDomain)
        {
            UnityEngine_Debug_CLRRedirection.RegisterILRuntimeCLRRedirection(appDomain);
            UnityEngine_GameObject_CLRRedirection.RegisterILRuntimeCLRRedirection(appDomain);
            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appDomain);
        }


        /// <summary>
        /// 注册CLR重定向(发版前固定)
        /// </summary>
        /// <param name="appDomain"></param>
        private static void InitializeValueTypeBinder(this ILRuntime.Runtime.Enviorment.AppDomain appDomain)
        {
            appDomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
            appDomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
            appDomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        }
    }
}