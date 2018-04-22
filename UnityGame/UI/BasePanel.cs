using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public class BasePanel : EventDispatcher
    {

        public const string GUILOADED = "guiloaded";
        // Use this for initialization


        public void CreateUI(string name, string res)
        {

            //StartCoroutine(RealLoadGUI(name, res));

        }

        private void OnloadGUIComplete(string name, GameObject go, object callbackData)
        {
            Hashtable ret = new Hashtable();
            ret.Add("name", callbackData);
            ret.Add("obj", go);

            //DispatchEvent(new EventEx(GUILOADED, ret));
        }

        private void OnloadGUIProgress(string name, float progress, object callbackData)
        {
            //
        }

        private IEnumerator RealLoadGUI(string name, string res)
        {
            yield return new WaitForEndOfFrame();

            Hashtable ret = new Hashtable();
            ret.Add("name", name);

            //DispatchEvent(new EventEx(GUILOADED, ret));
            yield return 0;
        }
    }

}

