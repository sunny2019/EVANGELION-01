using UnityEngine;

namespace Mode
{
    [DisallowMultipleComponent]
    public class Init : MonoBehaviour
    {
        private async void Awake()
        {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
            await ModeDebugger.Init();
#endif
            await ModeAddressables.HotUpdate();
            await ModeILRuntime.Init();
            
            Destroy(gameObject);
        }
    }
}