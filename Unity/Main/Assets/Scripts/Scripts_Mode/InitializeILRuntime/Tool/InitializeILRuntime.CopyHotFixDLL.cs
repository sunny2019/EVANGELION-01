#if UNITY_EDITOR
namespace ILRuntime.Moudle
{
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// 负责Copy Unity 编译完成的HotFixDLL
    /// </summary>
    public static class CopyHotFixDLL
    {
        [UnityEditor.Callbacks.DidReloadScripts(0)]
        static void OnScriptReload()
        {
            
            string dllPath = Directory.GetParent(Application.dataPath) + $"/Library/ScriptAssemblies/{InitializeILRuntime.HotFixDLLName}.dll";
            string pdbPath = Directory.GetParent(Application.dataPath) + $"/Library/ScriptAssemblies/{InitializeILRuntime.HotFixDLLName}.pdb";
            if (File.Exists(dllPath) && File.Exists(pdbPath))
            {
                File.Copy(dllPath,Directory.GetParent(Application.dataPath)+"/" +InitializeILRuntime.dllBytesPath,true);
                File.Copy(pdbPath,Directory.GetParent(Application.dataPath)+"/" +InitializeILRuntime.pdbBytesPath,true);
            }
            else
            {
                Debug.LogError("请在InitializeILRuntime.Config.cs中配置正确的热更dll和pdb文件路径。");
            }
        }
    }
}
#endif