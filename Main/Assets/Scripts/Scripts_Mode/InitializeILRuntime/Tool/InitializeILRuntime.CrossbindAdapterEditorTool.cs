#if UNITY_EDITOR
namespace ILRuntime.Moudle
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 负责跨域继承适配器和适配器的注册文件生成
    /// </summary>
    public static partial class InitializeILRuntime
    {
        [UnityEditor.MenuItem("Tools/ILRuntime/生成跨域继承适配器(发版前固定)")]
        private static void InitializeILRuntimeCrossbindAdapter()
        {
            if (Directory.Exists(crossBindingAdaptorPath))
                Directory.Delete(crossBindingAdaptorPath, true);
            Directory.CreateDirectory(crossBindingAdaptorPath);
            GenerateCrossbindAdapter();
            GenerateCrossbindAdapterInitialize();
            UnityEditor.AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成跨域继承适配器
        /// </summary>
        private static void GenerateCrossbindAdapter()
        {
            if (list_GenerateCrossbindAdapterTypes.Count != 0)
            {
                string filePath;
                for (int i = 0; i < list_GenerateCrossbindAdapterTypes.Count; i++)
                {
                    filePath = string.IsNullOrEmpty(list_GenerateCrossbindAdapterTypes[i].Namespace) ? "" : (list_GenerateCrossbindAdapterTypes[i].Namespace + "_").Replace('.', '_');
                    filePath = Path.Combine(crossBindingAdaptorPath, filePath + list_GenerateCrossbindAdapterTypes[i].Name + "_CrossbindAdapter.cs");
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath))
                    {
                        sw.WriteLine(ILRuntime.Runtime.Enviorment.CrossBindingCodeGenerator.GenerateCrossBindingAdapterCode(list_GenerateCrossbindAdapterTypes[i], crossBindingAdaptorNameSpace));
                    }

                    filePath = String.Empty;
                }
            }
        }

        private static void GenerateCrossbindAdapterInitialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"namespace ILRuntime.Moudle
{");
            if (list_GenerateCrossbindAdapterTypes.Count != 0)
            {
                sb.AppendLine($"    using {crossBindingAdaptorNameSpace};");
                sb.AppendLine("");
            }

            sb.AppendLine(@"    /// <summary>
    /// 负责跨域继承适配器的注册(自动生成文件，请勿手动更改)
    /// </summary>
    public static partial class InitializeILRuntime
    {
        /// <summary>
        /// 注册跨域继承适配器(发版前固定)
        /// </summary>
        private static void InitializeCrossbindAdapter(this ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {");
            for (int i = 0; i < list_GenerateCrossbindAdapterTypes.Count; i++)
            {
                sb.AppendLine($@"            appdomain.RegisterCrossBindingAdaptor(new {list_GenerateCrossbindAdapterTypes[i].Name}Adapter());");
            }

            sb.AppendLine(@"        }");
            sb.AppendLine(@"        /// <summary>
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
}");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(crossBindingAdaptorPath, crossBindingAdaptorRegisterFileName)))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
#endif