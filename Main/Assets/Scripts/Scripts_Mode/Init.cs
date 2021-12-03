namespace Mode
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public class Init : MonoBehaviour
    {
        private async void Awake()
        {
#if DEBUG
            await ModeDebugger.Init();
#endif
            await ModeAddressables.HotUpdate();
            await ModeILRuntime.Init();

            Destroy(gameObject);
        }
    }
}