using System.Collections.Generic;

namespace UnityGameToolkit
{
    public interface IObserver
    {
        string ObserverName { get; }

        IList<string> ListNotificationInterests();

        int HandleNotification(Notification notification);
    }
}
