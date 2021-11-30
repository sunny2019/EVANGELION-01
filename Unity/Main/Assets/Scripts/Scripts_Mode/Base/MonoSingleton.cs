namespace Mode
{
    using System;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

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