using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class SmartObject : UObject
    {
        protected Dictionary<string, IComponent> mCompDic = new Dictionary<string, IComponent>();
        protected List<IComponent> mCompList = new List<IComponent>();
        protected void AddComponent(string compName, IComponent component)
        {
            if (!mCompDic.ContainsKey(compName))
            {
                mCompDic[compName] = component;
                mCompList.Add(component);
            }
        }

        protected void RemoveComponent(string compName)
        {
            if (mCompDic.ContainsKey(compName))
            {
                IComponent component = mCompDic[compName];
                mCompList.Remove(component);
                component.Destroy();
            }
        }

        protected IComponent Retain(string compName)
        {
            if (mCompDic.ContainsKey(compName))
            {
                return mCompDic[compName];
            }

            return null;
        }

        protected void RemoveAllComponent()
        {
            foreach (KeyValuePair<string, IComponent> kvp in mCompDic)
            {
                kvp.Value.Destroy();
            }

            mCompDic.Clear();
            mCompList.Clear();
        }

        protected virtual void Destroy()
        {
            RemoveAllComponent();
        }

        void Update()
        {
            for (int i = 0; i < mCompList.Count; ++i)
            {
                if (mCompList[i] != null)
                {
                    mCompList[i].OnUpdate();
                }
            }
        }
    }
}


