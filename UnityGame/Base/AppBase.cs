using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class AppBase : MonoBehaviour, IApp, IBindable
    {
        private GlobalManager _global;

        public virtual int Initialize()
        {
            _global = new GlobalManager();
            _global.Create();

            return 1;
        }

        private void Awake()
        {
            Initialize();
        }

        public virtual int Start()
        {
            _global.Start();

            return 1;
        }

        private void Update()
        {
            _global.Update();
        }

        public virtual void Stop()
        {
            _global.Stop();
        }

        public virtual void Pause()
        {
            _global.Stop();
        }

        public virtual void Resume()
        {
            _global.Start();
        }

        public virtual void Destroy()
        {
            _global.DestroyDirectly();
        }

        public string Register(string uniqueName, IManager mgr)
        {
            return _global.Register(uniqueName, mgr);
        }

        public void UnRegister(string uniqueName, bool bStop = true, bool bDestroy = true)
        {
            _global.UnRegister(uniqueName, bStop, bDestroy);
        }

        public IManager GetMGR(string uniqueName)
        {
            return _global.GetMGR(uniqueName);
        }
    }
}


