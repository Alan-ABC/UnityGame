using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class BaseGUIMediator : BaseMediator
    {

        public const string Name = "BaseGUIMediator";

        protected GameObject panel;
        protected GameObject view;
        public string wndName = ""; //窗体的名字
        protected bool isClosed = true;
        public int wndWidth = 0;        //窗体尺寸
        public int wndHeight = 0;
        public bool top = false;            //在当前层的最上面
        //public WNDLayerSort layerIndex = WNDLayerSort.BASE; // 处在的层级
        public bool unique = false;     //是否独占(如果独占，则打开当前界面，当前层上的其它界面会被关闭)
        public bool uniqueAllLayer = false; //是否在所有层独占(如果独占，则打开当前界面，所有层上的其它界面会被关闭)
        public string uiPath = "";          //ui路径
        public string uilayer = "MainUI/Camera/gameui/"; //ui放在哪一层
        public int depth = 0;
        public bool active = true;
        public int timestamp = 0; // 排序时间戳
        public static Dictionary<string, string> WnameMedname = new Dictionary<string, string>();
        protected object args = null;
        public bool toggleOpen = false;//2次点击这个按钮,第一次打开第二次关闭
        public bool addGoBack = false;//添加后退按钮
        public bool autoOpen = false;//返回MainSceneUI的时候是否要自动打开
        public bool showFightValue = false;//打开某些界面,会把最上面的体力和精力隐藏显示经验和战斗力


        public BaseGUIMediator(string _NAME = Name) : base(_NAME)
        {
        }

        public static string GetMedNameByWname(string wName)
        {
            if (WnameMedname.ContainsKey(wName))
            {
                return WnameMedname[wName];
            }
            return "";
        }
        public void InitDic(string _name)
        {
            if (!WnameMedname.ContainsKey(wndName))
            {
                WnameMedname.Add(wndName, _name);
            }
        }
        public override IList<string> ListNotificationInterests()
        {
            List<string> interest = new List<string>
        {
            Command.OPENUI,
            Command.CLOSEUI,
            Command.UI_INIT,
            Command.UI_CLEAR
        };

            interest.AddRange(base.ListNotificationInterests());

            return interest;
        }

        public override bool ProcMsg(Notification notification)
        {

            switch (notification.Type)
            {
                case Command.OPENUI:
                    WndManager.Instance.OpenWindow(NAME, notification.Data);
                    return true;
                case Command.CLOSEUI:
                    CloseUI(notification.Data);

                    return true;
                //让基类继续处理
                case Command.UI_INIT:
                    WndManager.Instance.RegisterWindow(this);
                    OnInit();
                    return true;
                case Command.UI_CLEAR:
                    WndManager.Instance.UnRegisterWindow(this);
                    OnDestroy();
                    return true;
                //case Command.ATTACH_PANEL:
                    //return true;
                //case Command.DETACH_PANEL:
                    //return true;
                default:
                    return base.ProcMsg(notification);
            }

        }


        public void OpenUI(object param)
        {
            isClosed = false;
            args = param;

            panel = GameObject.Find(uilayer);
            if (view == null)
            {
                BasePanel gp = panel.GetComponent<BasePanel>();
                //gp[BasePanel.GUILOADED] += new EventDispatch.EventHandler(LoadUIDown);
                gp.CreateUI(wndName, uiPath);
            }
            else
            {
                ShowUI();
            }


        }


        public void LoadUIDown(Event e)
        {
            Hashtable evt = e.Data as Hashtable;

            string name = "";
            GameObject obj = null;
            name = evt["name"] as string;

            if (name != wndName)
            {
                return;
            }

            BasePanel gp = panel.GetComponent<BasePanel>();

            //gp[BasePanel.GUILOADED] -= new EventDispatcher.EventHandle(LoadUIDown);

            if (evt.ContainsKey("obj"))
            {
                obj = evt["obj"] as GameObject;
                view = obj;
                ShowUI();
            }
            else
            {
                Debug.Log("加载ui失败");
                return;
            }

        }

        private void ShowUI()
        {
            if (view != null)
            {
                if (!isClosed)
                {
                    //NGUITools.SetActive(view, active);
                    OnOpenUI();
                    WndManager.Instance.SortWindowDepth();
                }
                else
                {
                    CloseUI(null);
                }
            }
        }

        public void CloseUI(object param)
        {
            isClosed = true;

            if (view != null)
            {
                OnCloseUI();
                WndManager.Instance.CloseWindow(NAME, false);

                GameObject.DestroyImmediate(view);
                view = null;
            }
        }

        public virtual void OnOpenUI()
        {

        }

        public virtual void OnCloseUI()
        {

        }

        public virtual int SetDepth(int dep)
        {
            depth = dep;
            if (view != null)
            {
                BaseWindow bw = view.GetComponent<BaseWindow>();
                return bw.OnChangeDepth(depth);
            }

            return depth;
        }
        // 某个GameObject是否是该窗口上的
        public bool HaveChildWnd(GameObject btn)
        {
            if (view == null)
                return false;
            if (btn.transform.IsChildOf(view.transform))
                return true;
            else
                return false;
        }

        public override IList<string> InterestPropertys()
        {
            List<string> interest = new List<string>
            {

            };
            interest.AddRange(base.InterestPropertys());

            return interest;
        }

        /*public override void OnChangeProperty(ObjectID objid, GameAttr attr)
        {
            if (view != null)
                ChangeProperty(objid, attr);
        }

        public virtual void ChangeProperty(ObjectID objid, GameAttr attr)
        {
        }

        public virtual void Undo()
        {

        }*/


    }

}

