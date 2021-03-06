namespace ILRuntime.Moudle
{
    public static partial class InitializeILRuntime
    {
#if UNITY_EDITOR
        private static string clrBindingPath = "Assets/Scripts/Scripts_Mode/InitializeILRuntime/CLRBindingGenerate";
        private static string clrBindingRegisterFileName = "InitializeILRuntime.CLRBindingGenerate.cs";

        private static string crossBindingAdaptorPath = "Assets/Scripts/Scripts_Mode/InitializeILRuntime/CrossBindingAdaptorGenerate";
        private static string crossBindingAdaptorRegisterFileName = "InitializeILRuntime.CrossbindAdapterGenerate.cs";
        private static string crossBindingAdaptorNameSpace = "ILRuntime.Moudle.CrossbindAdapter";

        public static string HotFixDLLName = "HotFix";
        public static string dllBytesPath = $"Assets/Scripts/HotFixDLL/{HotFixDLLName}Dll.bytes";
        public static string pdbBytesPath = $"Assets/Scripts/HotFixDLL/{HotFixDLLName}Pdb.bytes";
#endif
    }
}