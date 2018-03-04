using UnityEngine;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class TimerManger : SingleManagerBase
    {
        // 
        public class TimerContent
        {
            public delegate void TickFunc(object args);
            public float sTime;
            public float eTime;
            public float delay;
            public int tickCount;
            public float interval;
            public object args;
            public TickFunc callback;
            public bool needRemove;
            public string timerID;
            public int currTickCount;
        }

        private List<TimerContent> _runTimers = new List<TimerContent>();
        private Dictionary<string, TimerContent> _timerMap = new Dictionary<string, TimerContent>();
        private List<TimerContent> _removeList = new List<TimerContent>();
        private int _len;

        void Update()
        {
            if (_runTimers.Count > 0)
            {
                _len = _runTimers.Count;

                for (int i = 0; i < _len; ++i)
                {
                    if (!_runTimers[i].needRemove)
                    {
                        //print("####:" + _runTimers[i].eTime + "::" + Time.time);
                        if (_runTimers[i].eTime <= Time.time)
                        {
                            if (_runTimers[i].callback != null)
                            {
                                _runTimers[i].callback.Invoke(_runTimers[i].args);
                                _runTimers[i].currTickCount++;
                            }

                            if (_runTimers[i].currTickCount >= _runTimers[i].tickCount)
                            {
                                _runTimers[i].needRemove = true;
                                _removeList.Add(_runTimers[i]);
                            }
                            else
                            {
                                _runTimers[i].eTime = Time.time + _runTimers[i].interval;
                            }
                        }
                    }
                    else
                    {
                        _removeList.Add(_runTimers[i]);
                    }
                }
            }

            if (_removeList.Count > 0)
            {
                _len = _removeList.Count;
                //print("####");

                for (int i = 0; i < _len; ++i)
                {
                    _removeList[i].callback = null;
                    _timerMap.Remove(_removeList[i].timerID);
                    _runTimers.Remove(_removeList[i]);
                }

                _removeList.Clear();
            }
            
        }

        public void AddTimer(string timerId, float startTime, float endTime, float delayTime, float intervalTime, TimerContent.TickFunc call, object pArgs, int count)
        {
            if (_timerMap.ContainsKey(timerId))
            {
                print("timerID重名了");
                return;
            }

            TimerContent item = new TimerContent();
            item.timerID = timerId;
            item.tickCount = count;
            item.callback = call;
            item.delay = delayTime;
            item.currTickCount = 0;

            if (startTime < Time.time)
            {
                item.sTime = Time.time;
            }
            else
            {
                item.sTime = startTime;
            }

            if (endTime < Time.time)
            {
                item.eTime = Time.time;
            }
            else
            {
                item.eTime = endTime;
            }

            if (delayTime > 0)
            {
                item.eTime += delayTime;
                //print("@@@:" + item.eTime);
            }

            item.args = pArgs;
            item.interval = intervalTime;


            _runTimers.Add(item);
            _timerMap[timerId] = item;
        }

        public void AddTimer(string timerId, float delayTime, TimerContent.TickFunc call, object pArgs = null)
        {
            AddTimer(timerId, 0f, 0f, delayTime, 0, call, pArgs, 1);
        }

        public void AddTimer(string timerId, float delayTime, float intervalTime, int count, TimerContent.TickFunc call, object pArgs = null)
        {
            AddTimer(timerId, 0f, 0f, delayTime, intervalTime, call, pArgs, count);
        }

        public void RemoveTimer(string timerId)
        {
            if (_timerMap.ContainsKey(timerId))
            {
                _timerMap[timerId].needRemove = true;
            }
        }

        public void ResetTimer(string timerId)
        {
            if (_timerMap.ContainsKey(timerId))
            {
                _timerMap[timerId].eTime = Time.time + _timerMap[timerId].delay;
            }
            else
            {
                print("Can't find this timer.  May be it's destroied or not created");
            }
        }

        public bool HasTimer(string timerId)
        {
            return _timerMap.ContainsKey(timerId);
        }

        public void RemoveTimer(TimerContent item)
        {
            RemoveTimer(item.timerID);
        }
    }
}

