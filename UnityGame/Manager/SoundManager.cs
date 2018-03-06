using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameToolkit
{
    public class SoundManager : SingleManagerBase
    {
        private AudioSource mAudio;
        private Hashtable sounds = new Hashtable();

        void Start()
        {
            mAudio = GetComponent<AudioSource>();
        }

        /// <summary>
        /// ���һ������
        /// </summary>
        void Add(string key, AudioClip value)
        {
            if (sounds[key] != null || value == null) return;
            sounds.Add(key, value);
        }

        /// <summary>
        /// ��ȡһ������
        /// </summary>
        AudioClip Get(string key)
        {
            if (sounds[key] == null) return null;
            return sounds[key] as AudioClip;
        }

        /// <summary>
        /// ����һ����Ƶ
        /// </summary>
        public AudioClip LoadAudioClip(string path)
        {
            AudioClip ac = Get(path);

            if (ac == null)
            {
                ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
                Add(path, ac);
            }
            return ac;
        }

        /// <summary>
        /// �Ƿ񲥷ű������֣�Ĭ����1������
        /// </summary>
        /// <returns></returns>
        public bool CanPlayBackSound()
        {
            string key = AppConf.AppPrefix + "BackSound";
            int i = PlayerPrefs.GetInt(key, 1);
            return i == 1;
        }

        /// <summary>
        /// ���ű�������
        /// </summary>
        /// <param name="canPlay"></param>
        public void PlayBacksound(string name, bool canPlay)
        {
            if (mAudio.clip != null)
            {
                if (name.IndexOf(mAudio.clip.name) > -1)
                {
                    if (!canPlay)
                    {
                        mAudio.Stop();
                        mAudio.clip = null;
                        Resources.UnloadUnusedAssets();
                    }
                    return;
                }
            }

            if (canPlay)
            {
                mAudio.loop = true;
                mAudio.clip = LoadAudioClip(name);
                mAudio.Play();
            }
            else
            {
                mAudio.Stop();
                mAudio.clip = null;
                Resources.UnloadUnusedAssets();
            }
        }

        /// <summary>
        /// �Ƿ񲥷���Ч,Ĭ����1������
        /// </summary>
        /// <returns></returns>
        public bool CanPlaySoundEffect()
        {
            string key = AppConf.AppPrefix + "SoundEffect";
            int i = PlayerPrefs.GetInt(key, 1);
            return i == 1;
        }

        /// <summary>
        /// ������Ƶ����
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        public void Play(AudioClip clip, Vector3 position)
        {
            if (!CanPlaySoundEffect()) return;
            AudioSource.PlayClipAtPoint(clip, position); 
        }
    }
}