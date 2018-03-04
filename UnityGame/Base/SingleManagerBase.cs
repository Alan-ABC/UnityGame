using UnityEngine;
using System.Collections;
using System;

namespace UnityGameToolkit
{
    public class SingleManagerBase : MonoBehaviour, IManagement
    {
        private void Start()
        {
            Global.Get().Add(this.GetType(), this);

            OnInit();
        }

        private void OnDestroy()
        {
            Global.Get().Remove(this.GetType());

            OnDispose();
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnDispose()
        {

        } 

        public virtual void Reset()
        {
            
        }

        public virtual void Destroy()
        {

        }
    }
}

