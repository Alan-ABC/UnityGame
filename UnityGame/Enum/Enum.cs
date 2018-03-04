using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public enum ServerStatus
    {
        SS_DESTROY,
        SS_STOP,
        SS_RUNNING
    }

    public enum LogLevel
    {
        LL_INFO,
        LL_WARNING,
        LL_ERROR
    }

    public enum DisType
    {
        Exception,
        Disconnect,
    }
}
