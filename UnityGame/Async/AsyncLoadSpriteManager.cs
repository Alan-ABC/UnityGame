using UnityEngine;
using System.Collections.Generic;

public class AtlasConfig
{
    public Texture2D texture;
    public object atlas;
}

public static class AsyncLoadSpriteManager
{
    public delegate void SpriteCreateNextCall(Sprite sprite);
    private static Dictionary<string, Dictionary<string, Sprite>> sGroupSprite = new Dictionary<string, Dictionary<string, Sprite>>();
    private static Dictionary<string, Dictionary<string, List<SpriteCreateNextCall>>> sNextCallDic = new Dictionary<string, Dictionary<string, List<SpriteCreateNextCall>>>();
    private static Dictionary<string, List<string>> sGroupSpriteNameDic = new Dictionary<string, List<string>>();
    private static Dictionary<string, AtlasConfig> sAtlasDic = new Dictionary<string, AtlasConfig>();
    private static bool sStartAsyncCreate = false;
    private static Queue<string> sQueueCreate = new Queue<string>();

    private static string sCurrentGroup = string.Empty;
    private static List<string> sCurrentSpriteNames = null;
    private static int sSpriteNameIndex = 0;
    public static void AddSprite(string groupName, string spriteName, Sprite sprite)
    {
        if (sGroupSprite.ContainsKey(groupName))
        {
            if (!sGroupSprite[groupName].ContainsKey(spriteName))
            {
                sGroupSprite[groupName][spriteName] = sprite;
            }
        }
        else
        {
            sGroupSprite[groupName] = new Dictionary<string, Sprite>();
            sGroupSprite[groupName].Add(spriteName, sprite);
        }
    }

    public static Sprite GetSprite(string groupName, string spriteName)
    {
        if (sGroupSprite.ContainsKey(groupName))
        {
            if (sGroupSprite[groupName].ContainsKey(spriteName))
            {
                return sGroupSprite[groupName][spriteName];
            }
        }

        return null;
    }

    public static bool HasSprite(string groupName, string spriteName)
    {
        if (sGroupSprite.ContainsKey(groupName))
        {
            if (sGroupSprite[groupName].ContainsKey(spriteName))
            {
                return true;
            }
            
        }

        return false;
    }

    public static void AddNextCall(string groupName, string spriteName, SpriteCreateNextCall callback, Texture2D texture, object atlas)
    {
        if (sGroupSprite.ContainsKey(groupName))
        {
            if (sGroupSprite[groupName].ContainsKey(spriteName))
            {
                callback(sGroupSprite[groupName][spriteName]);
                return;
            }
            else
            {
                if (sNextCallDic[groupName].ContainsKey(spriteName))
                {
                    if (!sNextCallDic[groupName][spriteName].Contains(callback))
                    {
                        sNextCallDic[groupName][spriteName].Add(callback);
                    }
                }
                else
                {
                    sNextCallDic[groupName][spriteName] = new List<SpriteCreateNextCall>()
                    {
                        callback
                    };
                    sGroupSpriteNameDic[groupName].Add(spriteName);
                }
            }
        }
        else
        {
            sGroupSprite[groupName] = new Dictionary<string, Sprite>();
            sNextCallDic[groupName] = new Dictionary<string, List<SpriteCreateNextCall>>();
            sGroupSpriteNameDic[groupName] = new List<string>()
            {
                spriteName
            };

            List<SpriteCreateNextCall> calls = new List<SpriteCreateNextCall>()
            {
                callback
            };

            sNextCallDic[groupName].Add(spriteName, calls);
            AtlasConfig config = new AtlasConfig();
            config.texture = texture;
            config.atlas = atlas;
            sAtlasDic[groupName] = config;
        }

        AsyncCreateGroup(groupName);
    }

    public static void Reset()
    {
        sCurrentGroup = string.Empty;
        sCurrentSpriteNames = null;
        sSpriteNameIndex = 0;
}

    public static void AsyncCreateGroup(string groupName)
    {
        if (!sStartAsyncCreate)
        {
            BeginFrame(groupName);
            StartAsyncCreate();
        }
        else
        {
            if (sCurrentGroup == groupName || sQueueCreate.Contains(groupName))
            {
                return;
            }

            sQueueCreate.Enqueue(groupName);
        }
    }

    public static void StartAsyncCreate()
    {
        if (!sStartAsyncCreate)
        {
            sStartAsyncCreate = true;
            FrameUpdateManager.Instance.onFrame += OnEnterFrame;
        }
        
    }

    public static void StopAsyncCreate()
    {
        if (sStartAsyncCreate)
        {
            sStartAsyncCreate = false;
            FrameUpdateManager.Instance.onFrame -= OnEnterFrame;
        }
    }

    public static void OnEnterFrame()
    {
        NextFrame();
    }

    public static void BeginFrame(string groupName)
    {
        Reset();
        sQueueCreate.Enqueue(groupName);
        sCurrentGroup = sQueueCreate.Dequeue();

        if (sGroupSpriteNameDic.ContainsKey(groupName))
        {
            sCurrentSpriteNames = sGroupSpriteNameDic[groupName];
        }
        else
        {
            NextFrame(false);
        }
    }

    public static void NextFrame(bool waitComplete = true)
    {
        if (sCurrentSpriteNames == null || !waitComplete || sSpriteNameIndex >= sCurrentSpriteNames.Count)
        {
            sSpriteNameIndex = 0;

            if (sQueueCreate.Count > 0)
            {
                sCurrentGroup = sQueueCreate.Dequeue();

                if (sGroupSpriteNameDic.ContainsKey(sCurrentGroup))
                {
                    sCurrentSpriteNames = sGroupSpriteNameDic[sCurrentGroup];

                    if (sSpriteNameIndex >= sCurrentSpriteNames.Count)
                    {
                        NextFrame();
                        return;
                    }
                }
                else
                {
                    NextFrame();
                    return;
                }
                
            }
            else
            {
                StopAsyncCreate();
                return;
            }
            
        }

        string spriteName = sCurrentSpriteNames[sSpriteNameIndex];
        Sprite sp = CreateSprite(sCurrentGroup, spriteName);
        AddSprite(sCurrentGroup, spriteName, sp);
        CallFunc(sCurrentGroup, spriteName, sp);
        sSpriteNameIndex++;
    }

    public static void CallFunc(string groupName, string spriteName, Sprite sp)
    {
        List<SpriteCreateNextCall> callbacks = sNextCallDic[groupName][spriteName];

        if (callbacks != null)
        {
            foreach(SpriteCreateNextCall callback in callbacks)
            {
                if (callback != null)
                {
                    callback.Invoke(sp);
                }
            }

            callbacks.Clear();
        }
    }


    public static Sprite CreateSprite(string groupName, string spriteName)
    {
        if (sAtlasDic.ContainsKey(groupName))
        {
            /*UVData uvs = AtlasManager.Instance.GetUVData(sAtlasDic[groupName].atlas, spriteName);

            if (uvs != null)
            {
                Rect rect = new Rect(uvs.x, uvs.y, uvs.width, uvs.height);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Vector4 border = uvs.border;
                Sprite sp = Sprite.Create(sAtlasDic[groupName].texture, rect, pivot, 100.0f, 0, SpriteMeshType.Tight, border);
                return sp;
            }*/
        }


        return null;
        
    }

    public static void Clear(string groupName)
    {
        if (sStartAsyncCreate)
        {
            if (sCurrentGroup == groupName)
            {
                NextFrame(false);
            }
        }

        if (!sAtlasDic.ContainsKey(groupName))
        {
            return;
        }

        RemoveSprites(groupName);
        RemoveCallbacks(groupName);
        RemoveAtlas(groupName);
    }

    public static void RemoveSprites(string groupName)
    {
        if (sGroupSprite.ContainsKey(groupName))
        {
            Dictionary<string, Sprite> spriteDic = sGroupSprite[groupName];

            if (spriteDic != null)
            {

                foreach (KeyValuePair<string, Sprite> kvp in spriteDic)
                {
                    GameObject.Destroy(kvp.Value);
                }
                sGroupSprite.Remove(groupName);
            }
        }
    }

    public static void RemoveCallbacks(string groupName)
    {
        if (sNextCallDic.ContainsKey(groupName))
        {
            Dictionary<string, List<SpriteCreateNextCall>> spriteCallDic = sNextCallDic[groupName];

            if (spriteCallDic != null)
            {
                foreach (KeyValuePair<string, List<SpriteCreateNextCall>> kvp in spriteCallDic)
                {
                    kvp.Value.Clear();
                }

                sNextCallDic.Remove(groupName);
            }
        }
        
    }

    public static void RemoveAtlas(string groupName)
    {
        if (sAtlasDic.ContainsKey(groupName))
        {
            sAtlasDic[groupName].texture = null;
            sAtlasDic[groupName].atlas = null;
            sAtlasDic.Remove(groupName);
        }
    }

    public static void RemoveCallback(string groupName, string spriteName, SpriteCreateNextCall callback)
    {
        if (sNextCallDic.ContainsKey(groupName))
        {
            if (sNextCallDic[groupName].ContainsKey(spriteName))
            {
                if (sNextCallDic[groupName][spriteName].Contains(callback))
                {
                    sNextCallDic[groupName][spriteName].Remove(callback);
                }
            }
        }
        
    }


    public static void ClearAll()
    {
        StopAsyncCreate();
        foreach (KeyValuePair<string, Dictionary<string, Sprite>> kvp in sGroupSprite)
        {
            Clear(kvp.Key);
        }

        sQueueCreate.Clear();
    }
}
