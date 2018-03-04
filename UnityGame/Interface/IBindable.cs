using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityGameToolkit;

namespace UnityGameToolkit
{
    public interface IBindable
    {
        string Register(string uniqueName, IManager mgr);
        void UnRegister(string uniqueName, bool bStop = true, bool bDestroy = true);
        IManager GetMGR(string uniqueName);
    }
}
