using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    /// <summary>
    /// 智能对象
    /// </summary>
    public class SmartObject : UObject
    {
        /// <summary>
        /// 组件管理器
        /// </summary>
        protected Dictionary<string, IComponent> mComponentDic = new Dictionary<string, IComponent>();
        /// <summary>
        /// 组件管理器
        /// </summary>
        protected List<IComponent> mCompList = new List<IComponent>();

        /// <summary>
        /// 加入组件
        /// </summary>
        /// <param name="compName"></param>
        /// <param name="component"></param>
        protected void AddComponent(string compName, IComponent component)
        {
            if (!mComponentDic.ContainsKey(compName))
            {
                mComponentDic[compName] = component;
                mCompList.Add(component);
            }
        }

        /// <summary>
        /// 移除组件
        /// </summary>
        /// <param name="compName"></param>
        protected void RemoveComponent(string compName)
        {
            if (mComponentDic.ContainsKey(compName))
            {
                IComponent component = mComponentDic[compName];
                mCompList.Remove(component);
                component.Destroy();
            }
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="compName"></param>
        /// <returns></returns>
        protected IComponent Retain(string compName)
        {
            if (mComponentDic.ContainsKey(compName))
            {
                return mComponentDic[compName];
            }

            return null;
        }

        /// <summary>
        /// 移除所有组件
        /// </summary>
        protected void RemoveAllComponent()
        {
            foreach (KeyValuePair<string, IComponent> kvp in mComponentDic)
            {
                kvp.Value.Destroy();
            }

            mComponentDic.Clear();
            mCompList.Clear();
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        protected virtual void Destroy()
        {
            RemoveAllComponent();
        }

        /// <summary>
        /// 更新组件
        /// </summary>
        void Update()
        {
            for (int i = 0; i < mCompList.Count; ++i)
            {
                if (mCompList[i] != null)
                {
                    mCompList[i].OnUpdate();
                }
            }

            OnUpdate();
        }

        /// <summary>
        /// 重写更新
        /// </summary>
        protected virtual void OnUpdate()
        {

        }
    }
}


