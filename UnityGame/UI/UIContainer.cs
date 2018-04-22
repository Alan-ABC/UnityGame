using UnityEngine;
using System.Collections.Generic;

public class UIContainer : UIModule
{
    private Dictionary<string, UIModule> mSubModuleDic;

    public override void OpenChildByName(string moduleName)
    {
        base.OpenChildByName(moduleName);
    }

    public override void Close()
    {
        foreach (KeyValuePair<string, UIModule> kvp in mSubModuleDic)
        {
            if (kvp.Value != null)
            {
                kvp.Value.Detach();
                kvp.Value.Close();
            }
        }

        mSubModuleDic.Clear();

        base.Close();

    }

    public void LoadSubModule(string moduleName, object openData)
    {
        if (mSubModuleDic.ContainsKey(moduleName))
        {
            if (mSubModuleDic[moduleName] == null)
            {
                UIModuleManager.Instance.CreateUI(moduleName, openData, this.ModuleName, moduleName);
            }
        }
    }

    public void UnLoadSubModule(string moduleName)
    {
        if (mSubModuleDic.ContainsKey(moduleName))
        {
            mSubModuleDic.Remove(moduleName);
        }
    }

    public override void Show()
    {
        base.Show();

        foreach (KeyValuePair<string, UIModule> kvp in mSubModuleDic)
        {
            if (kvp.Value != null)
            {
                kvp.Value.Show();
            }
        }
    }

    public override void Hide()
    {
        base.Hide();

        foreach (KeyValuePair<string, UIModule> kvp in mSubModuleDic)
        {
            if (kvp.Value != null)
            {
                kvp.Value.Hide();
            }
        }
    }
}
