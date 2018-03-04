using UnityEngine;
using System.Collections;

namespace UnityGameToolkit
{
    public static class StringUtil
    {
        private static string[] chineseNumber = new string[]
        {
            "〇",
            "一",
            "二",
            "三",
            "四",
            "五",
            "六",
            "七",
            "八",
            "九",
            "十"
        };
        public static string FormatString(string original, params object[] args)
        {
            return string.Format(original, args);
        }

        public static string ParseToChineseTime(int num, string unitColor = "#000000", string textColor = "#000000")
        {
            string outstr = "";

            int day = num / (24 * 3600);
            int left = num % (24 * 3600);
            
            int hour = left / 3600;
            
            left = left % 3600;

            int minute = left / 60;
            
            left = left % 60;
            
            if (day > 0)
            {
                outstr += GetChineseUpper(day, textColor) + "<color=" + unitColor + ">天</color>";
            }

            if (hour > 0)
            {
                outstr += GetChineseUpper(hour, textColor) + "<color=" + unitColor + ">时</color>";
            }

            if (minute > 0)
            {
                outstr += GetChineseUpper(minute, textColor) + "<color=" + unitColor + ">分</color>";
            }

            if (left > 0)
            {
                outstr += GetChineseUpper(left, textColor) + "<color=" + unitColor + ">秒</color>";
            }

            if (string.IsNullOrEmpty(outstr))
            {
                outstr += "<color=" + textColor + ">" + chineseNumber[0] + "</color>" + "<color=" + unitColor + ">秒</color>";
            }

            return outstr;
        }

        public static string ParseToChineseUnit(int num, string textColor = "#000000")
        {
            string outstr = "";

            int wan = num / 10000;

            int left = num % 10000;

            if (wan > 0)
            {
                outstr += GetChineseUpper(wan, textColor) + "<color=" + textColor + ">万</color>";
            }

            outstr += GetChineseUpper(left, textColor);

            if (string.IsNullOrEmpty(outstr))
            {
                outstr += "<color=" + textColor + ">" + chineseNumber[0] + "</color>";
            }

            return outstr;
        }

        public static string GetChineseUpper(int num, string textColor = "#000000")
        {
            string upper = "";

            int wan = num / 10000;

            int left = num % 10000;

            int thousand = left / 1000;

            left = left % 1000;

            int hundred = left / 100;

            left = left % 100;

            int ten = left / 10;

            left = left % 10;
            bool[] flags = new bool[4]
            {
                false, false, false, false
            };

            if (wan > 0)
            {
                upper += "<color=" + textColor + ">" + chineseNumber[wan] + "万</color>";
                flags[0] = true;
            }

            if (thousand > 0)
            {
                upper += "<color=" + textColor + ">" + chineseNumber[thousand] + "千</color>";
                flags[1] = true;
            }

            if (hundred > 0)
            {
                upper += "<color=" + textColor + ">" + chineseNumber[hundred] + "百</color>";
                flags[2] = true;
            }

            if (ten > 0)
            {
                upper += (ten > 1 ? ("<color=" + textColor + ">" + chineseNumber[ten] + "十</color>") : "<color=" + textColor + ">十</color>");
                flags[3] = true;
            }

            if (left > 0)
            {
                bool hasValue = false;
                string outstr = "";

                for (int i = flags.Length - 1; i >= 0; --i)
                {
                    if (flags[i] == true)
                    {
                        hasValue = true;
                        break;
                    }
                    else
                    {
                        outstr += chineseNumber[0];
                    }
                }

                if (hasValue)
                {
                    upper += "<color=" + textColor + ">" + outstr + chineseNumber[left] + "</color>";
                }
                else
                {
                    upper += "<color=" + textColor + ">" + chineseNumber[left] + "</color>";
                }
                
            }

            return upper;
        }
    }
}

