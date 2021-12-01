using System;

namespace ILRuntime.Moudle
{
    public static partial class InitializeILRuntime
    {
        public static void InitializeCustom(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
            //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
            appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            appdomain.DebugService.StartDebugService(56000);
#endif
            //委托搭配及跨域委托适配器配置及注册（此方法内容手动填写）
            appdomain.InitializeDelegate();
            //跨域继承适配器注册（此方法内容自动生成）
            //配置在Initialize.CrossbindAdapter.cs中，填写类型并点击标题栏ILRuntimeMoudle/生成跨域继承适配器(发包大版本更新时使用)
            appdomain.InitializeCrossbindAdapter();
            appdomain.InitializeSpecialCrossbindAdapter();
            //CLR重定向的配置及注册
            appdomain.InitializeCLRRedirection();
            //值类型绑定重定向注册
            appdomain.InitializeValueTypeBinder();
            
            
            
            
            //CLR绑定初始化
            //自定义跨域继承适配器在InitializeILRuntime.CLRBinding.cs中，填写类型并点击标题栏ILRuntimeMoudle/通过自动分析热更DLL生成CLR绑定(发包大版本更新时使用)
            //请确定此步骤执行前已完成生成跨域继承适配器的配置和生成
            //如因CLR缺少配置导致此处方法丢失，请注释并修改配置重新生成CLRBinding后解注
            
            appdomain.InitializeCLRBinding();
            
        }
    }
}