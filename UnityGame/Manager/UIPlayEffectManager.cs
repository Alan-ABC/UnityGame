using UnityEngine;
using System.Collections;

public delegate void EffectComplete();
public delegate void EffectError();
public class UIPlayEffectManager : MonoBehaviour
{

    public void PlayUIEffect(int effID)
    {

    }
}


public class EffectData
{
    public int effectID;
    public EffectComplete completeHandle;
    public EffectError errorHandle;
}
