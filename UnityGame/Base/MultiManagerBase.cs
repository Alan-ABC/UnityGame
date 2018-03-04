using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class MultiManagerBase : MonoBehaviour, IManagement
    {
        protected virtual void Start()
        {
            Global.Get().Add(this.name, this);
        }

        protected virtual void OnDestroy()
        {
            Destroy();
        }

        public virtual void Destroy()
        {
            Global.Get().Remove(this.name, true);
        }

        public void Reset()
        {

        }
    }
}

