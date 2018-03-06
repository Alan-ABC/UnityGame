using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class WndManager
    {

        private static WndManager _instance = null;
        Dictionary<string, BaseGUIMediator> m_regWnd = new Dictionary<string, BaseGUIMediator>();
        Dictionary<string, List<string>> m_OpenWnd = new Dictionary<string, List<string>>();

        public static WndManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WndManager();
                    _instance.Init();
                }

                return _instance;
            }
        }

        public void Init()
        {
            m_OpenWnd = new Dictionary<string, List<string>>();
        }

        public static void Dispose()
        {
            _instance = null;
        }

        public void RegisterWindow(BaseGUIMediator m)
        {
            if (m_regWnd.ContainsKey(m.NAME))
            {
                return;
            }

            m_regWnd.Add(m.NAME, m);

            if (!m_OpenWnd.ContainsKey(m.uilayer))
                m_OpenWnd.Add(m.uilayer, new List<string>());

        }

        public void UnRegisterWindow(BaseGUIMediator m)
        {
            if (m_regWnd.ContainsKey(m.NAME))
            {
                if (wndIsEnable(m.wndName))
                {
                    CloseWindow(m.wndName, true, true);
                }
                m_regWnd.Remove(m.NAME);
            }
        }

        public void OpenWindow(string wndName, object args = null)
        {
            if (!m_regWnd.ContainsKey(wndName))
            {
                return;
            }

            BaseGUIMediator bm = m_regWnd[wndName];
            bm.timestamp = Time.frameCount;

            if (m_OpenWnd[bm.uilayer].Contains(wndName))
            {
                if (bm.toggleOpen)
                {
                    CloseWindow(wndName, true, false);
                }
                return;
            }

            m_OpenWnd[bm.uilayer].Add(wndName);
            //回退按钮
            /*if (bm.addGoBack)
            {
                PanelHistoryMgr.Get().PushIn(bm);
            }*/
            //修改最上排的信息
            if (bm.showFightValue)
            {
                //Notifier.SendNotification(Command.SHOW_FIGHT_IN_PANMAIN, true, "MainSceneMediator");
            }

            //关闭独占的窗口
            //closeUniqueWnd(wndName, false);

            while (m_OpenWnd[bm.uilayer].Count > 5)
            {
                CloseWindow(m_OpenWnd[bm.uilayer][0], true, false);
            }

            if (bm.unique || bm.uniqueAllLayer)
            {
                CloseOtherWnd(wndName, false);
            }
            else
            {//只有是打开覆盖层的时候会出现覆盖在ROLEINFO主角模型上这种情况
                //Notifier.SendNotification(Command.HIDE_ROLEINFO_AVATAR, "", "RoleInfoMediator");
            }

            bm.OpenUI(args);
        }

        public void SortWindowDepth()
        {
            int dep = 20;

            foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
            {
                List<BaseGUIMediator> sortList = new List<BaseGUIMediator>();

                foreach (string s in open.Value)
                {
                    BaseGUIMediator bm = m_regWnd[s];
                    sortList.Add(bm);
                }

                //sortList.Sort(CompareFunc);

                foreach (BaseGUIMediator bm in sortList)
                {
                    dep = bm.SetDepth(dep);
                }
            }
        }
        // 更新窗口深度 传过来的是点击的按钮
        public void ResetWndDepth(GameObject btn)
        {
            BaseGUIMediator selectGUIMediator = null;
            foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
            {
                foreach (string s in open.Value)
                {
                    BaseGUIMediator bm = m_regWnd[s];
                    if (bm.HaveChildWnd(btn))
                    {
                        selectGUIMediator = bm;
                        break;
                    }
                }
            }

            if (selectGUIMediator != null)
            {
                ResetSortWindowDepth(selectGUIMediator);
            }
        }
        // 重新排序  传过来的放在最上面
        public void ResetSortWindowDepth(BaseGUIMediator selectGUIMediator)
        {
            int dep = 20;

            foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
            {
                List<BaseGUIMediator> sortList = new List<BaseGUIMediator>();
                foreach (string s in open.Value)
                {
                    BaseGUIMediator bm = m_regWnd[s];
                    if (!bm.Equals(selectGUIMediator))
                    {
                        sortList.Add(bm);
                    }
                }

                selectGUIMediator.timestamp = Time.frameCount;
                sortList.Add(selectGUIMediator);

                foreach (BaseGUIMediator bm in sortList)
                {
                    dep = bm.SetDepth(dep);
                }
            }
        }
        public void CloseWindow(string wndName, bool sendMsg, bool resort = false)
        {
            if (!m_regWnd.ContainsKey(wndName))
            {
                return;
            }

            BaseGUIMediator bm = m_regWnd[wndName];
            //回退
            /*if (bm.addGoBack)
            {
                PanelHistoryMgr.Get().Del(bm);
            }*/
            //隐藏RoleInfo界面模型
            if (!bm.unique)
            {
                //Notifier.SendNotification(Command.SHOW_ROLEINFO_AVATAR, "", "RoleInfoMediator");
            }
            //修改MainPan 顶部显示数据类型 战斗力or精力
            if (bm.showFightValue)
            {
                //Notifier.SendNotification(Command.SHOW_FIGHT_IN_PANMAIN, false, "MainSceneMediator");
            }

            if (m_OpenWnd[bm.uilayer].Contains(wndName))
            {
                if (sendMsg)
                {
                    Notifier.SendNotification(Command.CLOSEUI, null, wndName);
                }
                m_OpenWnd[bm.uilayer].Remove(wndName);
            }

            if (resort)
                SortWindowDepth();

        }

        public void CloseOtherWnd(string exclude, bool resort = false)
        {
            if (!m_regWnd.ContainsKey(exclude))
            {
                return;
            }

            BaseGUIMediator bm = m_regWnd[exclude];
            if (bm.uniqueAllLayer)
            {
                //处理所有的层
                foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
                {
                    foreach (string s in open.Value)
                    {
                        if (s != exclude)
                            Notifier.SendNotification(Command.CLOSEUI, null, s);
                    }
                    m_OpenWnd[bm.uilayer].Clear();

                }

                m_OpenWnd[bm.uilayer].Add(exclude);
            }
            else
            {
                string s;
                List<string> OpenTem = new List<string>();
                for (int i = 0; i < m_OpenWnd[bm.uilayer].Count; i++)
                {
                    OpenTem.Add(m_OpenWnd[bm.uilayer][i]);
                }
                for (int i = 0; i < OpenTem.Count; ++i)
                {
                    s = OpenTem[i];
                    if (s != exclude)
                    {
                        Notifier.SendNotification(Command.CLOSEUI, null, s);
                    }
                }
                m_OpenWnd[bm.uilayer].Clear();
                m_OpenWnd[bm.uilayer].Add(exclude);
            }

            if (resort)
                SortWindowDepth();

        }


        public void closeUniqueWnd(string exclude, bool resort = false)
        {
            string excuteLayer = "";
            if (exclude != "")
            {
                BaseGUIMediator bme = m_regWnd[exclude];
                excuteLayer = bme.uilayer;
            }

            //处理所有的层
            foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
            {
                foreach (string s in open.Value)
                {
                    if (s == exclude) continue;

                    BaseGUIMediator b = m_regWnd[s];
                    if (b.uniqueAllLayer)
                    {
                        Notifier.SendNotification(Command.CLOSEUI, null, s);
                        m_OpenWnd[open.Key].Remove(s);
                        break;
                    }
                    else if (b.unique && b.uilayer == excuteLayer)
                    {
                        Notifier.SendNotification(Command.CLOSEUI, null, s);
                        m_OpenWnd[open.Key].Remove(s);
                        break;
                    }

                }
            }

            if (resort)
                SortWindowDepth();
        }

        // 窗口是否正在显示
        public bool wndIsEnable(string wndName)
        {
            if (!m_regWnd.ContainsKey(wndName))
            {
                return false;
            }

            BaseGUIMediator bm = m_regWnd[wndName];

            bool bEnable = false;
            if (m_OpenWnd[bm.uilayer].Contains(wndName))
            {
                bEnable = true;
            }
            return bEnable;
        }

        // 关闭所有窗口
        static public bool bChangeReviveUIState = false;
        public void CloseAllWnd()
        {
            string strFirst = "";
            foreach (KeyValuePair<string, List<string>> open in m_OpenWnd)
            {
                List<string> listFirst = open.Value;
                if (listFirst.Count == 0)
                    continue;
                strFirst = listFirst[0];
                break;
            }
            CloseOtherWnd(strFirst);
            CloseWindow(strFirst, true);
        }

        /*public static int CompareFunc(BaseGUIMediator a, BaseGUIMediator b)
        {
            if ((int)a.layerIndex > (int)b.layerIndex)
                return 1;
            else if ((int)a.layerIndex < (int)b.layerIndex)
                return -1;
            else
            {
                if (a.timestamp > b.timestamp)
                    return 1;
                else if (a.timestamp < b.timestamp)
                    return -1;
                else
                    return 0;
            }
        }*/
    }
}

