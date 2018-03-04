using UnityEngine;
using System.Collections;

using UnityGameToolkit;

namespace UnityGameToolkit
{
    public class Notification : Event
    {

        public Notification(string type) : base(type)
        {

        }

        public Notification(string type, object data) : base(type, data)
        {

        }

        public Notification(string type, object data, object target) : base(type, data, target)
        {

        }
    }
}


