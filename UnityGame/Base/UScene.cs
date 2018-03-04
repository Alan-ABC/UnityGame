using UnityEngine;
using System.Collections;

using UnityGameToolkit;

namespace UnityGameToolkit
{
    public class UScene : DisplayObject, IScene
    {
        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public override void Awake()
        {
            base.Awake();

            OnEnter();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            OnExit();
        }
    }
}

