using UnityEngine;
using System.Collections;

public class UIModuleManager : MonoBehaviour
{
    public static UIModuleManager mInstance;

    public static UIModuleManager Instance
    {
        get
        {
            return mInstance;
        }
    }

    private void Awake()
    {
        mInstance = this;
    }
    
    public void CreateUI(string moduleName, object openData, string parentName = null, string subModuleName = null)
    {
        
    }

    public void DestroyUI()
    {

    }

    public void OnUpdate()
    {

    }
}

/// <summary>
/// UI状态
/// </summary>
public enum UIStatus
{
    UI_NONE, // 无
    UI_SHOW, // 显示中
    UI_HIDE, // 隐藏
    UI_POS_HIDE, // 位置隐藏
}
