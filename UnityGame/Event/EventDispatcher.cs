using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    public class EventDispatcher : MonoBehaviour, IEventDispatcher
    {
        public delegate void EventHandle(Event evt);

        private Dictionary<string, EventHandle> mCallFuncHandleDic = new Dictionary<string, EventHandle>();

        private void UnknowHandle(Event evt)
        {
            print("[EventDispatcher] - A undefined function be called. ");
        }

        private EventHandle this[string type]
        {
            get
            {
                if (mCallFuncHandleDic.ContainsKey(type))
                {
                    return mCallFuncHandleDic[type];
                }

                return new EventHandle(UnknowHandle);
            }
            set
            {
                mCallFuncHandleDic[type] = value;
            }
        }

        public void DispatchEvent(Event evt)
        {
            if (mCallFuncHandleDic.ContainsKey(evt.Type))
            {
                mCallFuncHandleDic[evt.Type].Invoke(evt);
            }
        }

        public void AddEventListener(string type, EventHandle handle)
        {
            if (!mCallFuncHandleDic.ContainsKey(type))
            {
                mCallFuncHandleDic[type] = handle;
            }
        }

        public void RemoveEventListener(string type)
        {
            if (mCallFuncHandleDic.ContainsKey(type))
            {
                mCallFuncHandleDic.Remove(type);
            }
        }

        public void Clear()
        {
            mCallFuncHandleDic.Clear();
        }
    }

}

