namespace Mode
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.AddressableAssets.ResourceLocators;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class ModeAddressables
    {
        /// <summary>
        /// 预加载使用此方法
        /// </summary>
        /// <param name="isInit">传入true则进行Addressables的初始化(用于启动更新)，传入false则不进行初始化(用于游戏中更新)</param>
        public static async UniTask HotUpdate(bool isInit = true)
        {
            List<object> keys = await CheckUpdate(isInit);
            Debug.Log("All Keys Count="+keys.Count);
            long sumSize = await GetDownloadSize(keys);
            Debug.Log("Download Sum Size="+sumSize);
            if (sumSize > 0)
            {
                await Download(keys, (l =>
                {
                    Debug.Log(l + "/" + sumSize);
                }));
            }
        }


        /// <summary>
        /// 确保当前为最新的Catalogs并且获取所有IResourceLocator
        /// 或者希望可以进行边玩边下时仅调用此方法即可而非HotUpdate
        /// </summary>
        /// <param name="isInit">是否初始化Addressables</param>
        /// <returns>Catalogs配置内容</returns>
        public static async UniTask<List<object>> CheckUpdate(bool isInit = true)
        {
            if (isInit)
            {
                var initialize = Addressables.InitializeAsync();
                await initialize.Task;
            }

            IEnumerable<IResourceLocator> locators = await CheckAndUpdateCataLogs();

            List<object> keys = new List<object>();
            foreach (var locator in locators)
                keys.AddRange(locator.Keys);

            return keys;
        }

        /// <summary>
        /// 检查Catalogs，获取最新的IResourceLocator集合
        /// </summary>
        /// <returns></returns>
        private static async UniTask<IEnumerable<IResourceLocator>> CheckAndUpdateCataLogs()
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


        /// <summary>
        /// 通过键集合获取需要下载内容的大小
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        private static async UniTask<long> GetDownloadSize(IEnumerable<object> keys)
        {
            var downloadSize = Addressables.GetDownloadSizeAsync(keys);
            await downloadSize.Task;
            return downloadSize.Result;
        }

        /// <summary>
        /// 通过键集合下载依赖的Bundle
        /// </summary>  
        /// <param name="keys"></param>
        /// <param name="downloadedSize"></param>
        private static async UniTask Download(IEnumerable<object> keys, Action<long> downloadedSize = null)
        {
            long allDownloadedSize = 0;
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
                        downloadedSize?.Invoke(allDownloadedSize+downloadDependencies.GetDownloadStatus().DownloadedBytes);
                        await UniTask.WaitForEndOfFrame();
                    }
                    allDownloadedSize += downloadDependencies.GetDownloadStatus().TotalBytes;
                }
            }
        }
    }
}