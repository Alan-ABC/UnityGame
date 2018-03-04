using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    public class EventDispatcher : MonoBehaviour, IEventDispatcher
    {
        public delegate void EventHandle(Event evt);

        private Dictionary<string, EventHandle> _handlesMap = new Dictionary<string, EventHandle>();

        private void UnknowHandle(Event evt)
        {
            print("[EventDispatcher] - A undefined function be called. ");
        }

        private EventHandle this[string type]
        {
            get
            {
                if (_handlesMap.ContainsKey(type))
                {
                    return _handlesMap[type];
                }

                return new EventHandle(UnknowHandle);
            }
            set
            {
                _handlesMap[type] = value;
            }
        }

        public void DispatchEvent(Event evt)
        {
            if (_handlesMap.ContainsKey(evt.Type))
            {
                _handlesMap[evt.Type].Invoke(evt);
            }
        }

        public void AddEventListener(string type, EventHandle handle)
        {
            if (!_handlesMap.ContainsKey(type))
            {
                _handlesMap[type] = handle;
            }
        }

        public void RemoveEventListener(string type)
        {
            if (_handlesMap.ContainsKey(type))
            {
                _handlesMap.Remove(type);
            }
        }

        public void Clear()
        {
            _handlesMap.Clear();
        }
    }

}

