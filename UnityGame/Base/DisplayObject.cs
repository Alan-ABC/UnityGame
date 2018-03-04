using UnityEngine;
using System.Collections;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    public class DisplayObject : MonoBehaviour, IDisplayObject
    {
        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {

        }

        public virtual void LateUpdate()
        {

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


