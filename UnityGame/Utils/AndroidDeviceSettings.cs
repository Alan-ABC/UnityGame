using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
public class AndroidDeviceSettings {

    public string applicationStorageFolder;
    private AndroidJavaClass m_unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidDeviceSettings s_instance;

    private AndroidDeviceSettings()
    {
        AndroidJavaObject @static = this.m_unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        this.applicationStorageFolder = @static.Call<AndroidJavaObject>("getFilesDir", new object[0]).Call<string>("getAbsolutePath", new object[0]);
        Debug.LogError(this.applicationStorageFolder);
    }

    public static AndroidDeviceSettings Get()
    {
        if (s_instance == null)
        {
            s_instance = new AndroidDeviceSettings();
        }
        return s_instance;
    }
}
#endif