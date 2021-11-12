using UnityEngine;

namespace ILRuntime.Moudle
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 负责跨域继承适配器和注册文件(发版前固定)
    /// </summary>
    public static partial class InitializeILRuntime
    {
#if UNITY_EDITOR

        /// <summary>
        /// 需要生成跨域继承适配器的类型（！！！这里是需要生成适配器的类型！！！）
        /// </summary>
        private static List<Type> list_GenerateCrossbindAdapterTypes = new List<Type>()
        {
        };
#endif
        /// <summary>
        /// 特殊的跨域继承适配器类型
        /// 如作者实现的CoroutineAdapter，因为Coroutine实现了多个接口，无法直接生成适配器，也无法注册CLRBinding,所以需要在CLRBinding前进行注册
        /// </summary>
        private static List<Type> list_SpecialCrossbindAdapterTypes = new List<Type>()
        {
            typeof(ILRuntime.Moudle.CrossBindingAdaptorCustom.MonoBehaviourAdapter),
            typeof(ILRuntime.Moudle.CrossBindingAdaptorCustom.CoroutineAdapter),
        };
    }
}