using UnityEngine;
using System.Collections.Generic;
using System;

namespace UnityGameToolkit
{
    public class Global : MonoBehaviour
    {
        private static Global mInstance;
        private Dictionary<string, IManagement> mManagerNameDic = new Dictionary<string, IManagement>();
        private Dictionary<Type, IManagement> mManagerTypeDic = new Dictionary<Type, IManagement>();

        private void Awake()
        {
            mInstance = this;
        }

        public static Global Get()
        {
            return mInstance;
        }

        public void Add(string name, IManagement manager)
        {
            if (!mManagerNameDic.ContainsKey(name))
            {
                mManagerNameDic[name] = manager;
            }
        }

        public void Add(Type type, IManagement manager)
        {
            if (!mManagerTypeDic.ContainsKey(type))
            {
                mManagerTypeDic[type] = manager;
            }
        }

        public void Remove(string name, bool destroy)
        {
            if (mManagerNameDic.ContainsKey(name))
            {
                if (destroy)
                {
                    mManagerNameDic[name].Destroy();
                }

                mManagerNameDic.Remove(name);
                
            }
        }

        public void Remove(Type type)
        {
            if (mManagerTypeDic.ContainsKey(type))
            {
                mManagerTypeDic.Remove(type);
            }
        }

        public T GetManager<T>(string name)  where T : class
        {
            if (mManagerNameDic.ContainsKey(name))
            {
                return mManagerNameDic[name] as T;
            }

            return default(T);
        }

        public T GetManager<T>() where T : class
        {
            Type type = typeof(T);

            if (mManagerTypeDic.ContainsKey(type))
            {
                return mManagerTypeDic[type] as T;
            }

            return default(T);
        }
    }
}

