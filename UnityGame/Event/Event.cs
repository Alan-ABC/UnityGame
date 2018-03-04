using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class Event
    {
        private string _type = null;
        private object _data = null;
        private object _target = null;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public object Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Event(string type)
        {
            Type = type;
        }

        public Event(string type, object data)
        {
            Type = type;
            Data = data;
        }

        public Event(string type, object data, object target)
        {
            Type = type;
            Data = data;
            Target = target;
        }
    }
}


