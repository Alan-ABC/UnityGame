using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityGameToolkit.Manager
{
    /// <summary>
    /// 游戏本地储存管理
    /// </summary>
    class ArchiveManager : SingleManagerBase
    {
        /// <summary>
        /// 本地读取int类型的值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <returns></returns>
        public int ReadIntProperty(string playerName, string propName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            return PlayerPrefs.GetInt(StringUtil.StringBuilder(playerName, propName), 0);
        }
        /// <summary>
        /// 本地读取string类型的值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <returns></returns>
        public string ReadStringProperty(string playerName, string propName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            return PlayerPrefs.GetString(StringUtil.StringBuilder(playerName, propName), "null");
        }
        /// <summary>
        /// 本地读取float类型的值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <returns></returns>
        public float ReadFloatProperty(string playerName, string propName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            return PlayerPrefs.GetFloat(StringUtil.StringBuilder(playerName, propName), 0.0f);
        }

        /// <summary>
        /// 保存一个int类型值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <param name="val">值</param>
        public void WriteIntProperty(string playerName, string propName, int val)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            PlayerPrefs.SetInt(StringUtil.StringBuilder(playerName, propName), val);
            PlayerPrefs.Save();
        }
        /// <summary>
        /// 保存一个string类型值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <param name="val">值</param>
        public void WriteStringProperty(string playerName, string propName, string val)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            PlayerPrefs.SetString(StringUtil.StringBuilder(playerName, propName), val);
            PlayerPrefs.Save();
        }
        /// <summary>
        /// 保存一个float类型值
        /// </summary>
        /// <param name="playerName">玩家名</param>
        /// <param name="propName">属性名</param>
        /// <param name="val">值</param>
        public void WriteFloatProperty(string playerName, string propName, float val)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new Exception("playerName is invalidate!");
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new Exception("propName is invalidate!");
            }

            PlayerPrefs.SetFloat(StringUtil.StringBuilder(playerName, propName), val);
            PlayerPrefs.Save();
        }
    }
}
