#define _DEBUG 
using UnityEngine;
using System.Collections;

using UnityGameToolkit;

namespace UnityGameToolkit
{
    /// <summary>
    /// 调试输出类
    /// </summary>
    public static class Ulog
    {
        public static void Log(string msg, LogLevel logLv = LogLevel.LL_INFO)
        {
#if UNITY_EDITOR || _DEBUG
            Output(msg, logLv);
#endif
        }

        public static void StrFormatLog(string str, params object[] parObjs)
        {
#if UNITY_EDITOR || _DEBUG
            Output(string.Format(str, parObjs));
#endif
        }

        public static void Log(params object[] str)
        {
#if UNITY_EDITOR || _DEBUG
            Output(string.Concat(str));
#endif
        }

        public static void StrFormatLog(string str, LogLevel logLv, params object[] parObjs)
        {
#if UNITY_EDITOR || _DEBUG
            Output(string.Format(str, parObjs), logLv);
#endif
        }

        public static void Log(LogLevel logLv, params object[] str)
        {
#if UNITY_EDITOR || _DEBUG
            Output(string.Concat(str), logLv);
#endif
        }

        private static void Output(string info, LogLevel level = LogLevel.LL_INFO)
        {
            switch (level)
            {
                case LogLevel.LL_ERROR:
                    Debug.LogError(info);
                    break;
                case LogLevel.LL_WARNING:
                    Debug.LogWarning(info);
                    break;
                case LogLevel.LL_INFO:
                    Debug.Log(info);
                    break;
                default:
                    Debug.Log(info);
                    break;
            }
        }

    }
}

