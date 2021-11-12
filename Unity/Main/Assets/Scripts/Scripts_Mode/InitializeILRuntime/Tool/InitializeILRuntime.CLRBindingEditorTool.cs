#if UNITY_EDITOR
namespace ILRuntime.Moudle
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 负责CLR绑定脚本生成
    /// </summary>
    public static partial class InitializeILRuntime
    {
        [UnityEditor.MenuItem("ILRuntimeMoudle/通过自动分析热更DLL生成CLR绑定(发版前固定)")]
        private static void InitializeILRuntimeCLRBinding()
        {
            if (Directory.Exists(clrBindingPath))
                Directory.Delete(clrBindingPath, true);
            Directory.CreateDirectory(clrBindingPath);
            if (!File.Exists(dllBytesPath))
            {
                Debug.LogError("请在InitializeILRuntime.Config.cs中配置正确的热更dll文件路径。");
            }
            bool needInitialize = GenerateCLRBinding(dllBytesPath, clrBindingPath);
            GenerateCLRBindingInitialize(needInitialize);
            UnityEditor.AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成CLR绑定脚本
        /// </summary>
        private static bool GenerateCLRBinding(string str_HotFixDLLPath, string str_CLRBindingPath)
        {
            List<Type> bindingTypes = new List<Type>();
            if (list_GenerateCrossbindAdapterTypes.Count != 0)
            {
                for (int i = 0; i < list_GenerateCrossbindAdapterTypes.Count; i++)
                {
                    Type type = Type.GetType(crossBindingAdaptorNameSpace + "." + list_GenerateCrossbindAdapterTypes[i].Name + "Adapter");
                    if (type != null && !bindingTypes.Contains(type))
                    {
                        bindingTypes.Add(Type.GetType(crossBindingAdaptorNameSpace + "." + list_GenerateCrossbindAdapterTypes[i].Name + "Adapter"));
                    }
                    else
                    {
                        Debug.LogError("未定位到适配器脚本：" + list_GenerateCrossbindAdapterTypes[i].FullName);
                    }
                }
            }

            if (list_SpecialCrossbindAdapterTypes.Count != 0)
            {
                for (int i = 0; i < list_SpecialCrossbindAdapterTypes.Count; i++)
                {
                    if (!bindingTypes.Contains(list_SpecialCrossbindAdapterTypes[i]))
                    {
                        bindingTypes.Add(list_SpecialCrossbindAdapterTypes[i]);
                    }
                }
            }

            if (bindingTypes.Count != 0)
            {
                ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
                using (System.IO.FileStream fs = new System.IO.FileStream(str_HotFixDLLPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    domain.LoadAssembly(fs);
                    for (int i = 0; i < bindingTypes.Count; i++)
                    {
                        domain.RegisterCrossBindingAdaptor(System.Activator.CreateInstance(bindingTypes[i]) as Runtime.Enviorment.CrossBindingAdaptor);
                    }

                    ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, str_CLRBindingPath);
                }

                return true;
            }

            return false;
        }

        private static void GenerateCLRBindingInitialize(bool needInitialize)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace ILRuntime.Moudle
{
    /// <summary>
    /// 负责CLR绑定注册(自动生成文件，请勿手动更改)
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册CLR绑定(发版前固定)
        /// </summary>
        private static void InitializeCLRBinding(this ILRuntime.Runtime.Enviorment.AppDomain appDomain)
        {");
            if (needInitialize)
                sb.AppendLine(@"            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);");
            sb.AppendLine(@"        }
    }
}");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(clrBindingPath, clrBindingRegisterFileName)))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
#endif