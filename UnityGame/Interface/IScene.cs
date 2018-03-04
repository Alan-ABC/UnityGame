using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityGameToolkit;

namespace UnityGameToolkit
{
    public interface IScene
    {
        void OnEnter();
        void OnExit();
    }
}
