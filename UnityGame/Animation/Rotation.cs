using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    /// <summary>
    /// 自转动画
    /// </summary>
    public class Rotation : AnimateBase
    {
        /// <summary>
        /// 自转的物体
        /// </summary>
        public GameObject[] objects;
        /// <summary>
        /// 自转方向
        /// </summary>
        public Vector3 rotateDir;
        /// <summary>
        /// 物体个数
        /// </summary>
        private int _objectCount;
        
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Start()
        {
            base.Start();

            _objectCount = objects.Length;

            if (autoPlay)
            {
                Play();
            }
        }

        /// <summary>
        /// 更新动画
        /// </summary>
        protected override void Update()
        {
            base.Update();

            if (_running)
            {
                for (int i = 0; i < _objectCount; ++i)
                {
                    objects[i].transform.Rotate(rotateDir);
                }
            }
        }
    }
}


