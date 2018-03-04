using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    /// <summary>
    /// 闪烁动画
    /// </summary>
    public class Blink : AnimateBase
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        private bool _showFlag;
        /// <summary>
        /// 动画间隔
        /// </summary>
        private float _delay;

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Start()
        {
            base.Start();

            if (frameRate <= 0)
            {
                frameRate = 1;
            }

            _delay = 1000f / frameRate;
        }

        /// <summary>
        /// 更新动画
        /// </summary>
        protected override void Update()
        {
            if (_running)
            {
                if ((Time.time - _lastTime) >= _delay)
                {
                    _lastTime = Time.time;

                    _showFlag = !_showFlag;

                    if (once && _showFlag)
                    {
                        _running = false;
                    }

                    target.SetActive(_showFlag);
                }
            }
        }
    }
}


