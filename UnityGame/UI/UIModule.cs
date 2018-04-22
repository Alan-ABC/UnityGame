using UnityEngine;
using System.Collections;

/// <summary>
/// UI模块类
/// </summary>
public class UIModule : MonoBehaviour
{
    private UIStatus mStatus;
    private bool mAsync;
    private bool mInject;
    private object mOpenData;
    private bool mPlayShowEffect;
    private bool mPlayHideEffect;
    protected Transform mParent;
    protected object mConfig;
    public string mModuleName;

    /// <summary>
    /// 打开界面时需要的数据
    /// </summary>
    /// <param name="args"></param>
    public void OpenData(object args)
    {
        mOpenData = args;

        if (Async)
        {
            StartCoroutine(AsyncRetrieveObjects());
        }
        else
        {
            RetrieveObjects();
        }
    }

    public void SetUIConfig(object config)
    {
        mConfig = config;
    }

    public void Attach(Transform toParent)
    {
        mParent = toParent;
        this.transform.SetParent(toParent);
        UnityGameToolkit.TransformUtil.ResetTransform(this.transform);
    }

    public virtual void OpenChildByName(string moduleName)
    {

    }

    public void Detach()
    {

    }

    IEnumerator AsyncRetrieveObjects()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        RetrieveObjects();
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    protected virtual void RetrieveObjects()
    {
        if (Async)
        {
            StartCoroutine(AsyncAddEvent());
        }
        else
        {
            AddEvent();
        }
    }

    IEnumerator AsyncAddEvent()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        AddEvent();
    }

    /// <summary>
    /// 释放组件
    /// </summary>
    protected virtual void ReleaseObjects()
    {
        RemoveEvent();
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    protected virtual void AddEvent()
    {

    }

    /// <summary>
    /// 移除事件
    /// </summary>
    protected virtual void RemoveEvent()
    {

    }

    /// <summary>
    /// 显示界面前处理
    /// </summary>
    protected virtual void BeforeShow()
    {

    }

    public virtual void Show()
    {
        BeforeShow();

        if (mPlayShowEffect)
        {

        }
        else
        {
            AfterShow();
        }
    }

    /// <summary>
    /// 显示界面后处理
    /// </summary>
    protected virtual void AfterShow()
    {
        InitDatabase();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    protected virtual void InitDatabase()
    {

    }

    /// <summary>
    /// 帧循环
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    /// <summary>
    /// 界面隐藏前处理
    /// </summary>
    protected virtual void BeforeHide()
    {

    }

    public virtual void Hide()
    {
        BeforeHide();

        if (mPlayHideEffect)
        {

        }
        else
        {
            AfterHide();
        }
    }

    /// <summary>
    /// 界面隐藏后处理
    /// </summary>
    protected virtual void AfterHide()
    {

    }

    /// <summary>
    /// 界面关闭
    /// </summary>
    public virtual void Close()
    {
        if (Async)
        {
            this.StopAllCoroutines();
        }
        
        mOpenData = null;
    }

    /// <summary>
    /// 当时UI状态
    /// </summary>
    public UIStatus UIStatus
    {
        get
        {
            return mStatus;
        }

        set
        {
            mStatus = value;
        }
    }

    /// <summary>
    /// 此界面是否内嵌界面
    /// </summary>
    public bool Inject
    {
        get
        {
            return mInject;
        }
        set
        {
            mInject = value;
        }
    }

    /// <summary>
    /// 是否异步打开
    /// </summary>
    public bool Async
    {
        get
        {
            return mAsync;
        }
        set
        {
            mAsync = value;
        }
    }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string ModuleName
    {
        get
        {
            return mModuleName;
        }
        set
        {
            mModuleName = value;
        }
    }

}
