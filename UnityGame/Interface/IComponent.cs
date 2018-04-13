using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public interface IComponent
    {
        void Initialize();

        void Destroy();

        void OnUpdate();
        
    }
}

