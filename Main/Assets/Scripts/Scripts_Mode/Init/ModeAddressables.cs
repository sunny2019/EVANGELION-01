namespace Mode
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.AddressableAssets.ResourceLocators;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class ModeAddressables
    {
        private static HotUpdate_Canvas hotUpdateUI;

        /// <summary>
        /// 预加载使用此方法
        /// </summary>
        /// <param name="isInit">传入true则进行Addressables的初始化(用于启动更新)，传入false则不进行初始化(用于游戏中更新)</param>
        public static async UniTask HotUpdate(bool isInit = true)
        {
            if (hotUpdateUI == null)
                hotUpdateUI=  (await Addressables.InstantiateAsync("Assets/Prefab/Synchronize/Prefab/HotUpdate_Canvas.prefab").Task).GetComponent<HotUpdate_Canvas>();
            hotUpdateUI.ResetUI();

            //CheckUpdate
            if (isInit)
            {
                hotUpdateUI.ShowTip("初始化中……");
                var initialize = Addressables.InitializeAsync();
                await initialize.Task;
            }

            //CheckAndUpdateCataLogs
            hotUpdateUI.ShowTip("计算差异中……");
            IEnumerable<IResourceLocator> locators = await CheckAndUpdateCataLogs();

            List<object> keys = new List<object>();
            foreach (var locator in locators)
                keys.AddRange(locator.Keys);

            Debug.Log("All Keys Count=" + keys.Count);

            hotUpdateUI.ShowTip("计算更新资源大小……");
            //通过键集合获取需要下载内容的大小
            var downloadSize = Addressables.GetDownloadSizeAsync((IEnumerable<object>) keys);
            await downloadSize.Task;
            long sumSize = downloadSize.Result;

            hotUpdateUI.HideTip();

            Debug.Log("Download Sum Size=" + sumSize);
            if (sumSize > 0)
            {
                //Download:通过键集合下载依赖的Bundle
                long allDownloadedSize = 0;
                hotUpdateUI.ShowProgress(allDownloadedSize, sumSize);
                    
                foreach (var key in keys)
                {
                    //TODO: 此处检查大小获取是否需要下载，keys过多消耗可能比较大。直接使用Keys调用DownloadDependenciesAsync进行下载虽然可以节省此部分性能，但是会导致无法在运行过程中进行热更。
                    var keySize = Addressables.GetDownloadSizeAsync(key);
                    await keySize.Task;
                    if (keySize.Result > 0)
                    {
                        var downloadDependencies = Addressables.DownloadDependenciesAsync(key);
                        while (!downloadDependencies.IsDone)
                        {
                            hotUpdateUI.ShowProgress(allDownloadedSize + downloadDependencies.GetDownloadStatus().DownloadedBytes, sumSize);
                            await UniTask.WaitForEndOfFrame();
                        }

                        //Debug.Log(key+"\t"+downloadDependencies.GetDownloadStatus().TotalBytes);
                        allDownloadedSize += downloadDependencies.GetDownloadStatus().TotalBytes;
                        hotUpdateUI.ShowProgress(allDownloadedSize, sumSize);
                        Addressables.Release(downloadDependencies);
                    }
                }

                hotUpdateUI.HideProgress();
            }

            if (hotUpdateUI != null)
                Object.Destroy(hotUpdateUI.gameObject);
        }

       

        /// <summary>
        /// 检查Catalogs，获取最新的IResourceLocator集合
        /// </summary>
        /// <returns></returns>
        public static async UniTask<IEnumerable<IResourceLocator>> CheckAndUpdateCataLogs()
        {
            IEnumerable<IResourceLocator> locators = Addressables.ResourceLocators;
            AsyncOperationHandle<List<string>> checkCatalogs = Addressables.CheckForCatalogUpdates();
            await checkCatalogs.Task;
            if (checkCatalogs.Result.Count != 0)
            {
                //Update Catalogs
                AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(checkCatalogs.Result);
                await updateHandle.Task;
                locators = updateHandle.Result;
            }
            // else
            //     Non-Update Catalogs

            return locators;
        }
    }
}