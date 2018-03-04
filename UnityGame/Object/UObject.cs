using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class UObject : MonoBehaviour, IDisplayObject
    {

        public virtual UObject Clone()
        {
            return null;
        }

        public bool Visible
        {
            get
            {
                return this.gameObject.activeSelf;
            }
            set
            {
                this.gameObject.SetActive(value);
            }
        }
    }
}

