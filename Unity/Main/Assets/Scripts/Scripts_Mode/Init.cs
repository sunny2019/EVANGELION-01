using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Mode
{
    [DisallowMultipleComponent]
    public class Init : MonoBehaviour
    {
        [SerializeField] private bool RealMode = false;
        public static AppDomain Appdomain;
        private static MemoryStream dllMs;
        private static MemoryStream pdbMs;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            if (!RealMode)
            {
#if UNITY_EDITOR
                LoadHotFixAssemblyEditor();
#else
                StartCoroutine(LoadHotFixAssemblyRuntime());
#endif
            }
            else
            {
                StartCoroutine(LoadHotFixAssemblyRuntime());
            }
        }

        private void OnDestroy()
        {
            if (dllMs != null)
                dllMs.Close();
            if (pdbMs != null)
                pdbMs.Close();
            dllMs = null;
            pdbMs = null;
        }

#if UNITY_EDITOR
        private static void LoadHotFixAssemblyEditor()
        {
            dllMs = new MemoryStream(File.ReadAllBytes(Directory.GetParent(Application.dataPath) + "/" + ILRuntime.Moudle.InitializeILRuntime.dllBytesPath));

            pdbMs = new MemoryStream(File.ReadAllBytes(Directory.GetParent(Application.dataPath) + "/" + ILRuntime.Moudle.InitializeILRuntime.pdbBytesPath));

            Appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            try
            {
                Appdomain.LoadAssembly(dllMs, pdbMs, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch
            {
                Debug.LogError("加载热更DLL失败，请确保编译过热更DLL");
            }

            ILRuntime.Moudle.InitializeILRuntime.InitializeCustom(Appdomain);
            OnHotFixLoaded();
        }
#endif


        IEnumerator LoadHotFixAssemblyRuntime()
        {
            WWW www = new WWW(Application.streamingAssetsPath + "/" + "HotFix.bytes");
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] dll = www.bytes;
            www.Dispose();
            dllMs = new MemoryStream(dll);

#if UNITY_EDITOR
            www = new WWW(Application.streamingAssetsPath + "/" + "HotFix.pdb");
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] pdb = www.bytes;
            www.Dispose();
            pdbMs = new MemoryStream(pdb);
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


        private static void OnHotFixLoaded()
        {
            Appdomain.Invoke("HotFix.Init", "HotFixInit", null, null);
        }
    }
}