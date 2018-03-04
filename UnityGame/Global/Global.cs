using UnityEngine;
using System.Collections.Generic;
using System;

namespace UnityGameToolkit
{
    public class Global : MonoBehaviour
    {
        private static Global _instance;
        private Dictionary<string, IManagement> _nameToMgr = new Dictionary<string, IManagement>();
        private Dictionary<Type, IManagement> _typeToMgr = new Dictionary<Type, IManagement>();

        private void Awake()
        {
            _instance = this;
        }

        public static Global Get()
        {
            return _instance;
        }

        public void Add(string name, IManagement manager)
        {
            if (!_nameToMgr.ContainsKey(name))
            {
                _nameToMgr[name] = manager;
            }
        }

        public void Add(Type type, IManagement manager)
        {
            if (!_typeToMgr.ContainsKey(type))
            {
                _typeToMgr[type] = manager;
            }
        }

        public void Remove(string name, bool destroy)
        {
            if (_nameToMgr.ContainsKey(name))
            {
                if (destroy)
                {
                    _nameToMgr[name].Destroy();
                }

                _nameToMgr.Remove(name);
                
            }
        }

        public void Remove(Type type)
        {
            if (_typeToMgr.ContainsKey(type))
            {
                _typeToMgr.Remove(type);
            }
        }

        public T GetManager<T>(string name)  where T : class
        {
            if (_nameToMgr.ContainsKey(name))
            {
                return _nameToMgr[name] as T;
            }

            return default(T);
        }

        public T GetManager<T>() where T : class
        {
            Type type = typeof(T);

            if (_typeToMgr.ContainsKey(type))
            {
                return _typeToMgr[type] as T;
            }

            return default(T);
        }
    }
}

