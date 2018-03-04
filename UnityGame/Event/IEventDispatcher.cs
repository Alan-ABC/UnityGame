using UnityGameToolkit;

namespace UnityGameToolkit
{
    public interface IEventDispatcher
    {
        void DispatchEvent(Event evt);

        void AddEventListener(string type, EventDispatcher.EventHandle handle);

        void RemoveEventListener(string type);
    }
}


