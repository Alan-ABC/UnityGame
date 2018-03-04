using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class ManagerBase : IManager, IRecyclable
    {
        private ServerStatus _state = ServerStatus.SS_DESTROY;

        public ServerStatus Status
        {
            get
            {
                return _state;
            }
        }

        public virtual void Create()
        {
            _state = ServerStatus.SS_STOP;
        }

        public virtual void Start()
        {
            if (_state == ServerStatus.SS_STOP)
            {
                _state = ServerStatus.SS_RUNNING;
            }  
        }

        public virtual void Stop()
        {
            _state = ServerStatus.SS_STOP;
        }

        public virtual void Destroy()
        {
            _state = ServerStatus.SS_DESTROY;
        }

        public virtual void DestroyDirectly()
        {
            if (_state == ServerStatus.SS_DESTROY)
            {
                return;
            }

            if (_state == ServerStatus.SS_RUNNING)
            {
                Stop();
            }

            Destroy();
        }

        public virtual void Update()
        {
            
        }

        public virtual void Reset()
        {
            
        }

        
    }
}


