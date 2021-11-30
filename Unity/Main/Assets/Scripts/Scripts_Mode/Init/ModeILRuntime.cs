namespace Mode
{
    using System.IO;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class ModeILRuntime : MonoSingleton<ModeILRuntime>
    {
        public static ILRuntime.Runtime.Enviorment.AppDomain Appdomain;
        private static MemoryStream dllMs;
        private static MemoryStream pdbMs;

        private async UniTask<byte[]> LoadILRuntimeFile(string aaKey)
        {
            var hotfix = Addressables.LoadAssetAsync<TextAsset>(aaKey);
            await hotfix.Task;
            byte[] bytes = hotfix.Result.bytes;
            Addressables.Release(hotfix);
            return bytes;
        }

        protected override async UniTask OnInit()
        {
            dllMs = new MemoryStream(await LoadILRuntimeFile("HotFixDll"));
#if DEBUG
            pdbMs = new MemoryStream(await LoadILRuntimeFile("HotFixPdb"));
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