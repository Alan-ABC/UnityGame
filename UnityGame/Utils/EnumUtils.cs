using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnityGameToolkit
{
    public class EnumUtils
    {
        private static Dictionary<System.Type, Dictionary<string, object>> s_enumCache = new Dictionary<System.Type, Dictionary<string, object>>();

        public static T GetEnum<T>(string str)
        {
            return GetEnum<T>(str, StringComparison.Ordinal);
        }

        public static T GetEnum<T>(string str, StringComparison comparisonType)
        {
            T local;
            if (!TryGetEnum<T>(str, comparisonType, out local))
            {
                throw new ArgumentException(string.Format("EnumUtils.GetEnum() - \"{0}\" has no matching value in enum {1}", str, typeof(T)));
            }
            return local;
        }

        public static string GetString<T>(T enumVal)
        {
            string name = enumVal.ToString();
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumVal.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length > 0)
            {
                return customAttributes[0].Description;
            }
            return name;
        }

        public static int Length<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        public static T Parse<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static T SafeParse<T>(string str)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), str);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static bool TryCast<T>(object inVal, out T outVal)
        {
            outVal = default(T);
            try
            {
                outVal = (T)inVal;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryGetEnum<T>(string str, out T outVal)
        {
            return TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
        }

        public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
        {
            Dictionary<string, object> dictionary;
            object obj2;
            System.Type key = typeof(T);
            s_enumCache.TryGetValue(key, out dictionary);
            if ((dictionary != null) && dictionary.TryGetValue(str, out obj2))
            {
                result = (T)obj2;
                return true;
            }
            IEnumerator enumerator = Enum.GetValues(key).GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    T current = (T)enumerator.Current;
                    if (GetString<T>(current).Equals(str, comparisonType))
                    {
                        if (dictionary == null)
                        {
                            dictionary = new Dictionary<string, object>();
                            s_enumCache.Add(key, dictionary);
                        }
                        if (!dictionary.ContainsKey(str))
                        {
                            dictionary.Add(str, current);
                        }
                        result = current;
                        return true;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable == null)
                {
                }
                disposable.Dispose();
            }
            result = default(T);
            return false;
        }
    }
}


