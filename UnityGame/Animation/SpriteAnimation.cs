using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityGameToolkit
{
    public sealed class SpriteAnimation : AnimateBase
    {
        public string[] fileSheet;
        //private UISprite _target;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            _currIndex = 0;
            _maxIndex = fileSheet.Length;
            //_target = target.GetComponent<UISprite>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (_running)
            {
                if ((Time.time - _lastTime) >= frameRate)
                {
                    _lastTime = Time.time;
                    _currIndex++;

                    if (_currIndex >= _maxIndex)
                    {
                        _currIndex = 0;

                        if (once)
                        {
                            _running = false;
                        }
                    }

                    //_target.spriteName = fileSheet[_currIndex];
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            _currIndex = 0;
        }

    }
}


