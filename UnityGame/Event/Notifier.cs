using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityGameToolkit;

namespace UnityGameToolkit
{
    public static class Notifier
    {
        private static Dictionary<string, IList<IObserver>> _observersMap = new Dictionary<string, IList<IObserver>>();
        private static Dictionary<string, IObserver> _regsMap = new Dictionary<string, IObserver>();

        public static void Clear()
        {
            _observersMap.Clear();
            _regsMap.Clear();
        }

        public static void AddObserver(IObserver obs)
        {
            if (_regsMap.ContainsKey(obs.ObserverName))
            {
                return;
            }

            _regsMap[obs.ObserverName] = obs;

            IList<string> interests = obs.ListNotificationInterests();

            foreach (string interest in interests)
            {
                if (!_observersMap.ContainsKey(interest))
                {
                    _observersMap[interest] = new List<IObserver>();
                }

                _observersMap[interest].Add(obs);
            }
        }

        public static void RemoveObserver(IObserver obs)
        {
            if (!_regsMap.ContainsKey(obs.ObserverName))
            {
                return;
            }

            _regsMap.Remove(obs.ObserverName);

            IList<string> interests = obs.ListNotificationInterests();

            foreach (string interest in interests)
            {
                if (_observersMap.ContainsKey(interest))
                {
                    IList<IObserver> obslist = _observersMap[interest];

                    foreach (IObserver ob in obslist)
                    {
                        if (ob.Equals(obs))
                        {
                            obslist.Remove(ob);
                            break;
                        }
                    }
                }
            }
        }

        public static IObserver RetrieveObserver(string obsName)
        {
            IObserver obs = null;

            _regsMap.TryGetValue(obsName, out obs);

            return obs;
        }

        public static void SendNotification(string type)
        {
            SendNotification(new Notification(type));
        }

        public static void SendNotification(string type, object data)
        {
            SendNotification(new Notification(type, data));
        }

        public static void SendNotification(string type, object data, object target)
        {
            SendNotification(new Notification(type, data, target));
        }

        public static void SendNotification(Notification notification)
        {
            IList<IObserver> obslist = null;

            if (_observersMap.TryGetValue(notification.Type, out obslist))
            {
                if (obslist != null && obslist.Count > 0)
                {
                    if (notification.Target != null)
                    {
                        string target = notification.Target as string;

                        foreach (IObserver obs in obslist)
                        {
                            if (obs.ObserverName == target)
                            {
                                if (obs.HandleNotification(notification) == -1)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (IObserver obs in obslist)
                        {
                            if (obs.HandleNotification(notification) == -1)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            
        }

    }

}
