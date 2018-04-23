using UnityEngine;
using System.Collections.Generic;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    internal sealed class GlobalManager : ManagerBase, IBindable
    {
        private Dictionary<string, IManager> mMangersDic = new Dictionary<string, IManager>();
        private uint mUniqueCount = 0;

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
            if (mMangersDic.ContainsKey(uniqueName))
            {
                uniqueName = uniqueName + "_" + mUniqueCount++;
            }

            if (mgr == null)
            {
                Debug.LogError("please make sure your manager can not be null, if you register your manager");
                return "null";
            }

            mMangersDic[uniqueName] = mgr;
            mgr.Create();

            return uniqueName;
        }

        public void UnRegister(string uniqueName, bool bStop = true, bool bDestroy = true)
        {
            if (mMangersDic.ContainsKey(uniqueName))
            {
                if (bStop)
                {
                    mMangersDic[uniqueName].Stop();
                }

                if (bDestroy)
                {
                    mMangersDic[uniqueName].DestroyDirectly();
                }
            }
        }

        public IManager GetMGR(string uniqueName)
        {
            if (mMangersDic.ContainsKey(uniqueName))
            {
                return mMangersDic[uniqueName];
            }

            return null;
        }


        public override void Create()
        {
            base.Create();
            mMangersDic.Clear();
            mUniqueCount = 0;
        }

        public override void Start()
        {
            base.Start();

            foreach (KeyValuePair<string, IManager> kv in mMangersDic)
            {
                kv.Value.Start();
            }
        }

        public override void Stop()
        {
            base.Stop();

            foreach (KeyValuePair<string, IManager> kv in mMangersDic)
            {
                kv.Value.Stop();
            }
        }

        public override void Reset()
        {
            
        }

        public override void Update()
        {
            if (mMangersDic.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, IManager> kv in mMangersDic)
            {
                if (kv.Value.Status == ServerStatus.SS_RUNNING)
                    kv.Value.Update();
            }
        }

        public override void Destroy()
        {
            base.Destroy();

            foreach (KeyValuePair<string, IManager> keyvalue in mMangersDic)
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

            mMangersDic.Clear();
            mUniqueCount = 0;
        }

        

    }
}


