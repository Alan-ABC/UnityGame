using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class AppBase : MonoBehaviour, IApp, IBindable
    {
        private GlobalManager mGlobal;

        public virtual int Initialize()
        {
            mGlobal = new GlobalManager();
            mGlobal.Create();

            return 1;
        }

        private void Awake()
        {
            Initialize();
        }

        public virtual int Start()
        {
            mGlobal.Start();

            return 1;
        }

        private void Update()
        {
            mGlobal.Update();
        }

        public virtual void Stop()
        {
            mGlobal.Stop();
        }

        public virtual void Pause()
        {
            mGlobal.Stop();
        }

        public virtual void Resume()
        {
            mGlobal.Start();
        }

        public virtual void Destroy()
        {
            mGlobal.DestroyDirectly();
        }

        public string Register(string uniqueName, IManager mgr)
        {
            return mGlobal.Register(uniqueName, mgr);
        }

        public void UnRegister(string uniqueName, bool bStop = true, bool bDestroy = true)
        {
            mGlobal.UnRegister(uniqueName, bStop, bDestroy);
        }

        public IManager GetMGR(string uniqueName)
        {
            return mGlobal.GetMGR(uniqueName);
        }
    }
}


