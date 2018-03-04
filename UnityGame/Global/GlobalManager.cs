using UnityEngine;
using System.Collections.Generic;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    internal sealed class GlobalManager : ManagerBase, IBindable
    {
        private Dictionary<string, IManager> _managerMap = new Dictionary<string, IManager>();
        private uint _uniqueCount = 0;

        public struct ManagerInfo
        {
            public IManager mgr;
            public bool bMarkRemove;
            public bool bStop;
            public bool bDestroy;
        }

        private ManagerInfo GenerateInfo(IManager mgr)
        {
            ManagerInfo info;
            info.mgr = mgr;
            info.bMarkRemove = false;
            info.bStop = false;
            info.bDestroy = false;

            return info;
        }

        public string Register(string uniqueName, IManager mgr)
        {
            if (_managerMap.ContainsKey(uniqueName))
            {
                uniqueName = uniqueName + "_" + _uniqueCount++;
            }

            if (mgr == null)
            {
                Debug.LogError("please make sure your manager can not be null, if you register your manager");
                return "null";
            }

            _managerMap[uniqueName] = mgr;
            mgr.Create();

            return uniqueName;
        }

        public void UnRegister(string uniqueName, bool bStop = true, bool bDestroy = true)
        {
            if (_managerMap.ContainsKey(uniqueName))
            {
                if (bStop)
                {
                    _managerMap[uniqueName].Stop();
                }

                if (bDestroy)
                {
                    _managerMap[uniqueName].DestroyDirectly();
                }
            }
        }

        public IManager GetMGR(string uniqueName)
        {
            if (_managerMap.ContainsKey(uniqueName))
            {
                return _managerMap[uniqueName];
            }

            return null;
        }


        public override void Create()
        {
            base.Create();
            _managerMap.Clear();
            _uniqueCount = 0;
        }

        public override void Start()
        {
            base.Start();

            foreach (KeyValuePair<string, IManager> kv in _managerMap)
            {
                kv.Value.Start();
            }
        }

        public override void Stop()
        {
            base.Stop();

            foreach (KeyValuePair<string, IManager> kv in _managerMap)
            {
                kv.Value.Stop();
            }
        }

        public override void Reset()
        {
            
        }

        public override void Update()
        {
            if (_managerMap.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, IManager> kv in _managerMap)
            {
                if (kv.Value.Status == ServerStatus.SS_RUNNING)
                    kv.Value.Update();
            }
        }

        public override void Destroy()
        {
            base.Destroy();

            foreach (KeyValuePair<string, IManager> keyvalue in _managerMap)
            {

                if (keyvalue.Value.Status != ServerStatus.SS_DESTROY)
                {
                    if (keyvalue.Value.Status == ServerStatus.SS_RUNNING)
                    {
                        keyvalue.Value.Stop();
                    }

                    keyvalue.Value.Destroy();
                }
                
            }

            _managerMap.Clear();
            _uniqueCount = 0;
        }

        

    }
}


