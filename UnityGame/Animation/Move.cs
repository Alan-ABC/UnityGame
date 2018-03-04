using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    /// <summary>
    /// 移动动画
    /// </summary>
    public class Move : AnimateBase
    {
        /// <summary>
        /// 目标位置
        /// </summary>
        public Vector3 destPosition;
        /// <summary>
        /// 是否往返
        /// </summary>
        public bool pingpong;

        /// <summary>
        /// 模拟位置 
        /// </summary>
        private Vector3 _simulatePosition;
        /// <summary>
        /// 原始位置
        /// </summary>
        private Vector3 _orginalPosition;
        /// <summary>
        /// 目标变幻
        /// </summary>
        private Transform _targetTf;

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Start()
        {
            base.Start();

            _targetTf = target.transform;
            _orginalPosition = _targetTf.localPosition;
            _simulatePosition = destPosition;
        }

        /// <summary>
        /// 更新动画
        /// </summary>
        protected override void Update()
        {
            if (_running)
            {
                _targetTf.localPosition = Vector3.Lerp(_targetTf.localPosition, _simulatePosition, frameRate * Time.deltaTime);

                if (Vector3.Distance(_targetTf.localPosition, _simulatePosition) < 0.05f)
                {
                    if (pingpong)
                    {
                        if (_simulatePosition == destPosition)
                            _simulatePosition = _orginalPosition;
                        else
                            _simulatePosition = destPosition;
                    }
                    else
                    {
                        _targetTf.localPosition = _orginalPosition;
                    }

                    if (once)
                    {
                        _running = false;
                    }
                }
            }
        }
    }
}


