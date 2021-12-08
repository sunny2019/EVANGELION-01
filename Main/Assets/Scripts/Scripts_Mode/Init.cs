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
                    await ModeAddressables.CheckUpdate();
                    break;
                default:
                    Addressables.LogError("当前AAMode:" + mAAMode + ",未进行对应模式初始化");
                    throw new ArgumentOutOfRangeException();
            }

            await ModeAddressables.HotUpdate();
            await ModeILRuntime.Init();

            Destroy(gameObject);
        }
    }
}