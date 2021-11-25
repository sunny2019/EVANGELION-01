using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class EasyUpdateAddressables : MonoBehaviour
{
    async void Start()
    {
        //C:\Users\changxinrui\AppData\LocalLow\DefaultCompany\AddressablesDemo\com.unity.addressables
        Debug.Log(Application.persistentDataPath);

        await HotUpdate();


        Transform uiParent = GameObject.Find("Canvas").transform;
        // AsyncOperationHandle op = Addressables.InstantiateAsync("remote_static", uiParent);
        // await op.Task;
        // Debug.Log("加载生成完成：remote_static");
        // op = Addressables.InstantiateAsync("remote_non_static", uiParent);
        // await op.Task;
        // Debug.Log("加载生成完成：remote_non_static");

        uiParent.Find("Button").GetComponent<Button>().onClick.AddListener(() => { Addressables.LoadSceneAsync("Game").Completed += (a) => { Addressables.InstantiateAsync("Capsule"); }; });
        uiParent.Find("Button_Check").GetComponent<Button>().onClick.AddListener(() => { HotUpdate(false); });
    }


    /// <summary>
    /// 预加载使用此方法
    /// </summary>
    /// <param name="isInit">传入true则进行Addressables的初始化(用于启动更新)，传入false则不进行初始化(用于游戏中更新)</param>
    public async UniTask HotUpdate(bool isInit=true)
    {
        List<object> keys = await CheckUpdate(isInit);
        long sumSize = await GetDownloadSize(keys);
        if (sumSize > 0)
        {
            long downloaded = 0;
            await Download(keys, (l =>
            {
                downloaded += l;
                Debug.Log(downloaded + "/" + sumSize);
            }));
        }
    }


    /// <summary>
    /// 确保当前为最新的Catalogs并且获取所有IResourceLocator
    /// 或者希望可以进行边玩边下时仅调用此方法即可而非HotUpdate
    /// </summary>
    /// <param name="isInit">是否初始化Addressables</param>
    /// <returns>Catalogs配置内容</returns>
    public async UniTask<List<object>> CheckUpdate(bool isInit = true)
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
    private async UniTask<IEnumerable<IResourceLocator>> CheckAndUpdateCataLogs()
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
    public async UniTask<long> GetDownloadSize(IEnumerable<object> keys)
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
    public async UniTask Download(IEnumerable<object> keys, Action<long> downloadedSize = null)
    {
        foreach (var key in keys)
        {
            var keySize = Addressables.GetDownloadSizeAsync(key);
            await keySize.Task;
            if (keySize.Result > 0)
            {
                var downloadDependencies = Addressables.DownloadDependenciesAsync(key);
                await downloadDependencies.Task;
                downloadedSize?.Invoke(keySize.Result);
            }
        }
    }
}