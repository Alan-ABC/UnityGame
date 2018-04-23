using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class Event
    {
        private string mType = null;
        private object mData = null;
        private object mTarget = null;

        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public object Data
        {
            get { return mData; }
            set { mData = value; }
        }

        public object Target
        {
            get { return mTarget; }
            set { mTarget = value; }
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


