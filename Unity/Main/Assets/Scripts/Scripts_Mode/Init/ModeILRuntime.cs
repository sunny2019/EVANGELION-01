using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Mode
{
    public class ModeILRuntime : MonoSingleton<ModeILRuntime>
    {
        public static AppDomain Appdomain;
        private static MemoryStream dllMs;
        private static MemoryStream pdbMs;

        protected override async UniTask OnInit()
        {
            var hotfixDll = Addressables.LoadAssetAsync<TextAsset>("HotFix.bytes");
            await hotfixDll.Task;
            byte[] dllBytes = hotfixDll.Result.bytes;
            Addressables.Release(hotfixDll);
            dllMs = new MemoryStream(dllBytes);
#if UNITY_EDITOR
            pdbMs = new MemoryStream(File.ReadAllBytes(Directory.GetParent(Application.dataPath) + "/" + ILRuntime.Moudle.InitializeILRuntime.pdbBytesPath));
#endif
            Appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            try
            {
                Appdomain.LoadAssembly(dllMs, pdbMs, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch
            {
                Debug.LogError("加载热更DLL失败，请确保已经通过编译过热更DLL");
            }

            ILRuntime.Moudle.InitializeILRuntime.InitializeCustom(Appdomain);
            OnHotFixLoaded();
        }

        protected override void Destroy()
        {
            if (dllMs != null)
                dllMs.Close();
            if (pdbMs != null)
                pdbMs.Close();
            dllMs = null;
            pdbMs = null;

            base.Destroy();
        }
        
        private static void OnHotFixLoaded()
        {
            Appdomain.Invoke("HotFix.Init", "HotFixInit", null, null);
        }
    }
}