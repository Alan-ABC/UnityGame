using UnityEngine;
using System.Collections.Generic;

public class AsyncOperator
{
    public delegate void DelayCall();

    private static Queue<AsyncOperator> sAsyncOperators = new Queue<AsyncOperator>();

    private int mFrameLoop = 0;
    private Queue<DelayCall> mDelayCalls = new Queue<DelayCall>();
    private bool mRunning = false;
    private int mWaitFrame = 1;

    public int WaitFrame
    {
        get
        {
            return mWaitFrame;
        }
        set
        {
            mWaitFrame = value;
        }
    }

    private static bool Contains(AsyncOperator ao)
    {
        return sAsyncOperators.Contains(ao);
    }

    public static AsyncOperator Retrieve()
    {
        if (sAsyncOperators.Count > 0)
        {
            return sAsyncOperators.Dequeue();
        }
        else
        {
            AsyncOperator ao = new AsyncOperator();
            return ao;
        }
    }

    public void Release()
    {
        Stop();

        
        if (!AsyncOperator.Contains(this))
        {
            sAsyncOperators.Enqueue(this);
        }
    }

    public void Push(DelayCall delayCall)
    {
        mDelayCalls.Enqueue(delayCall);
    }

    public void Pop(int count)
    {
        int temp = count;

        while (temp > 0)
        {
            if (mDelayCalls.Count > 0)
            {
                mDelayCalls.Dequeue();
            }
            else
            {
                break;
            }

            temp--;
        }
    }

    public void Play()
    {
        if (!mRunning)
        {
            mRunning = true;
            FrameUpdateManager.Instance.onFrame += OnEnterFrame;
        }
        
    }

    public void Stop(bool clearAll = true)
    {
        if (mRunning)
        {
            mRunning = false;
            FrameUpdateManager.Instance.onFrame -= OnEnterFrame;
        }

        if (clearAll)
        {
            ClearQueue();
        }
        
    }

    private void OnEnterFrame()
    {
        if (mFrameLoop >= mWaitFrame)
        {
            mFrameLoop = 0;
            if (mDelayCalls.Count > 0)
            {
                mDelayCalls.Dequeue().Invoke();
            }
            else
            {
                Stop();
            }
        }
        else
        {
            mFrameLoop++;
        }
    }

    private void ClearQueue()
    {
        if (mDelayCalls.Count > 0)
        {
            mDelayCalls.Clear();
        }
    }

}
