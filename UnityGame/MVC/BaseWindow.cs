using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class BaseWindow : EventDispatcher
    {

        public virtual int OnChangeDepth(int dep)
        {
            //UIPanel panel = gameObject.GetComponent<UIPanel>();
            /*UIPanel[] panels = gameObject.GetComponentsInChildren<UIPanel>(true);

            List<UIPanel> sortList = new List<UIPanel>();
            foreach (UIPanel pan in panels)
            {
                //if (pan.GetComponent<BaseWindow>() == null)
                StaticPanelDepth staticDep = pan.GetComponent<StaticPanelDepth>();
                if (staticDep != null)
                {
                    if (staticDep.staticDepth)
                    {
                        continue;
                    }
                }
                sortList.Add(pan);
            }

            sortList.Sort(UIPanel.CompareFunc);
            //sortList.Reverse();

            foreach (UIPanel pan in sortList)
            {
                pan.depth = dep++;
            }*/

            return dep;
        }



    }
}


