using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class BaseMediator : IObserver
    {

        public string NAME = "BaseMediator";

        public const string ROLE = "role";
        public const string PLAYER = "player";
        public const string NPC = "npc";
        public const string ITEM = "item";
        public const string PET = "pet";
        public const string _ = "all";

        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="rec"></param>
        public virtual void OnCreateTable(string rec)
        {

        }

        /// <summary>
        /// 清空表格
        /// </summary>
        /// <param name="rec"></param>
        public virtual void OnClear(string rec)
        {

        }

        public BaseMediator(string _NAME)
        {
            NAME = _NAME;
        }

        public string ObserverName
        {
            get
            {
                return NAME;
            }
        }

        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>
        {
            Command.GAME_INIT,
            Command.CLEARALL
        };
        }

        public int HandleNotification(Notification notification)
        {
            if (notification.Target != null &&
               notification.Target != "" &&
               !notification.Target.Equals(NAME))
            {
                return 0;
            }

            if (!ProcMsg(notification))
            {
                switch (notification.Type)
                {
                    case Command.GAME_INIT:
                        OnInit();
                        break;
                    case Command.CLEARALL:
                        OnDestroy();
                        break;
                }
            }

            return 1;

        }

        public virtual bool ProcMsg(Notification notification)
        {
            return false;
        }
        // 销毁
        public virtual void OnDestroy()
        {

        }
        // 初始化
        public virtual void OnInit()
        {

        }
        // ===============属性相关============= //
        public virtual IList<string> InterestPropertys()
        {
            return new List<string>
            {

            };
        }

    }

}

