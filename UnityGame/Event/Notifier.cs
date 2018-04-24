using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityGameToolkit;

public delegate void CallFuncHandle(object data);

namespace UnityGameToolkit
{
    public static class Notifier
    {
        private static Dictionary<string, List<CallFuncHandle>> mCallFuncDic = new Dictionary<string, List<CallFuncHandle>>();

        public static void Clear()
        {
            mCallFuncDic.Clear();
        }

        public static void AddEventListener(string msgName, CallFuncHandle callFunc)
        {
            if (mCallFuncDic.ContainsKey(msgName))
            {
                if (mCallFuncDic[msgName].Contains(callFunc))
                {
                    return;
                }
                else
                {
                    mCallFuncDic[msgName].Add(callFunc);
                }
            }
            else
            {
                mCallFuncDic[msgName] = new List<CallFuncHandle>()
                {
                    callFunc
                };
            }
        }

        public static void RemoveEventListener(string msgName, CallFuncHandle callFunc)
        {
            if (!mCallFuncDic.ContainsKey(msgName))
            {
                return;
            }

            if (mCallFuncDic[msgName].Contains(callFunc))
            {
                mCallFuncDic[msgName].Remove(callFunc);
            }
        }

        public static void Notification(string msgName, object data)
        {
            List<CallFuncHandle> callFuncs = null;

            if (mCallFuncDic.TryGetValue(msgName, out callFuncs))
            {
                if (callFuncs.Count > 0)
                {
                    foreach (CallFuncHandle func in callFuncs)
                    {
                        func.Invoke(data);
                    }
                }
            }

        }
    }

}
