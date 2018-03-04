using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    /// <summary>
    /// 动画基类
    /// </summary>
    public class AnimateBase : MonoBehaviour
    {
        /// <summary>
        /// 动画目标
        /// </summary>
        public GameObject target;
        /// <summary>
        /// 自动播放
        /// </summary>
        public bool autoPlay;
        /// <summary>
        /// 只播放一次
        /// </summary>
        public bool once;
        /// <summary>
        /// 播放速度
        /// </summary>
        public float frameRate;

        /// <summary>
        /// 是否已经播放
        /// </summary>
        protected bool _running;
        /// <summary>
        /// 当前次数
        /// </summary>
        protected int _currIndex;
        /// <summary>
        /// 最大次数
        /// </summary>
        protected int _maxIndex;
        /// <summary>
        /// 上次的时间
        /// </summary>
        protected float _lastTime;

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Start()
        {
            if (autoPlay)
            {
                _running = true;
            }
        }

        /// <summary>
        /// 更新函数（需要重写）
        /// </summary>
        protected virtual void Update()
        {

        }

        /// <summary>
        /// 播放动画
        /// </summary>
        public virtual void Play()
        {
            _running = true;
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public virtual void Stop()
        {
            _running = false;
            _currIndex = 0;
        }

        /// <summary>
        /// 暂停动画
        /// </summary>
        public virtual void Pause()
        {
            _running = false;
        }
    }
}


