using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
namespace UnityGameToolkit
{
    public class FileUtils
    {
        public static readonly char[] FOLDER_SEPARATOR_CHARS = new char[] { '/', '\\' };

        public static string BasePersistentDataPath
        {
            get
            {
                return UnityEngine.Application.persistentDataPath;
            }
        }

        public static string PersistentDataPath
        {
            get
            {
                string path = null;

                path = PublicPersistentDataPath;
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(string.Format("FileUtils.PersistentDataPath - Error creating {0}. Exception={1}", path, exception.Message));

                    }
                }
                return path;
            }
        }

        public static string PublicPersistentDataPath
        {
            get
            {
                return BasePersistentDataPath;
            }
        }

        public static string GameAssetPathToName(string path)
        {
            int num = path.LastIndexOf('/');
            if (num < 0)
            {
                return path;
            }
            return path.Substring(num + 1);
        }

        public static string GetAssetPath(string fileName)
        {
#if UNITY_EDITOR || UNITY_IOS
            return UnityEngine.Application.streamingAssetsPath + "/" + fileName;
#elif UNITY_ANDROID
        return (AndroidDeviceSettings.Get().applicationStorageFolder + "/" + fileName);
#endif
            return "";
        }

        /*public static string StripLocaleFromPath(string path)
        {
            string directoryName = System.IO.Path.GetDirectoryName(path);
            string fileName = System.IO.Path.GetFileName(path);
            if (Localer.IsValidLocaleName(System.IO.Path.GetFileName(directoryName)))
            {
                return string.Format("{0}/{1}", System.IO.Path.GetDirectoryName(directoryName), fileName);
            }
            return path;
        }

        public static string MakeLocalizedPathFromSourcePath(Locale locale, string path)
        {
            string directoryName = System.IO.Path.GetDirectoryName(path);
            string fileName = System.IO.Path.GetFileName(path);
            //int startIndex = directoryName.LastIndexOf("/");

            //if ((startIndex >= 0) && directoryName.Substring(startIndex + 1).Equals(Localization.))
            //{
            //    directoryName = directoryName.Remove(startIndex);
            //}
            return string.Format("{0}/{1}/{2}", directoryName, locale, fileName);
        }*/
    }

}
