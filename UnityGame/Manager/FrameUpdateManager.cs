using UnityEngine;
using System.Collections;

public class FrameUpdateManager : MonoBehaviour
{
    public delegate void FrameUpdate();
    public FrameUpdate onFrame;

    private static FrameUpdateManager _instance = null;
    public static FrameUpdateManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void OnUpdate()
    {
        if (onFrame != null)
        {
            onFrame();
        }
    }
}
