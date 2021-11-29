using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mode
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        // 单件子类实例
        private static T _instance;

        public static T Ins
        {
            get
            {
                
                if (_instance == null)
                {
                    Debug.LogError("MonoSingleton:" + typeof(T).Name + "未进行初始化。");
                }

                return _instance;
            }
        }

        /// <summary>
        ///     获得单件实例，查询场景中是否有该种类型，如果有存储静态变量，如果没有，构建一个带有这个component的gameobject
        ///     这种单件实例的GameObject直接挂接在bootroot节点下，在场景中的生命周期和游戏生命周期相同，创建这个单件实例的模块
        ///     必须通过DestroyInstance自行管理单件的生命周期
        /// </summary>
        /// <returns>返回单件实例</returns>
        public async static Task<T> Init()
        {
            if (_instance == null)
            {
                Type theType = typeof(T);

                _instance = (T) FindObjectOfType(theType);

                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);

                    _instance = go.AddComponent<T>();

                    //挂接到BootObj下
                    GameObject bootObj = GameObject.Find("EVANGELION-01");

                    if (bootObj == null)
                    {
                        bootObj = new GameObject("EVANGELION-01");
                        DontDestroyOnLoad(bootObj);
                    }

                    go.transform.SetParent(bootObj.transform);

                    await _instance.GetComponent<MonoSingleton<T>>().OnInit();
                }
                else
                {
                    Debug.LogError("MonoSingleton:" + typeof(T).Name + "已在当前场景中存在。");
                }
            }
            else
            {
                Debug.LogError("MonoSingleton:" + typeof(T).Name + "已完成初始化。");
            }


            return _instance;
        }

        protected virtual UniTask OnInit()
        {
            return UniTask.CompletedTask;
        }


        protected virtual void Destroy()
        {
            _instance = null;
        }
    }
}