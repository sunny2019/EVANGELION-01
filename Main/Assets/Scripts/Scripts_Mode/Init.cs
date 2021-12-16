namespace Mode
{
    using System;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [DisallowMultipleComponent]
    public class Init : MonoBehaviour
    {
        public enum AAMode
        {
            PreDownload,
            PlayingDownload,
        }

        [SerializeField] private AAMode mAAMode = AAMode.PreDownload;

        private async void Awake()
        {
#if DEBUG
            await ModeDebugger.Init();
#endif
            switch (mAAMode)
            {
                case AAMode.PreDownload:
                    await ModeAddressables.HotUpdate();
                    break;
                case AAMode.PlayingDownload:
                    await ModeAddressables. CheckAndUpdateCataLogs();
                    break;
                default:
                    Addressables.LogError("当前AAMode:" + mAAMode + ",未进行对应模式初始化");
                    throw new ArgumentOutOfRangeException();
            }

            await ModeILRuntime.Init();
            ModeILRuntime.Appdomain.Invoke("HotFix.Init", "HotFixInit", null, null);
            
            Destroy(gameObject);
        }

        /// <summary>
        /// 在运行过程中进行资源更新
        /// </summary>
        public static async void HotUpdate()
        {
            await ModeAddressables.HotUpdate(false);
            // TODO: 如果HotFixDll也更新了就需要重启应用程序
        }
    }
    
}