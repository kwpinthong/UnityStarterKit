using System;
using System.Collections;
using System.Collections.Generic;
using StarterKit.Common;
using UnityEngine;

namespace StarterKit.AudioManagerLib
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        
        public static void PlayBGM(string bgmId)
        {
            if (instance == null)
                CreateInstance.Create(nameof(AudioManager));
            instance.ThisPlayBGM(bgmId);
        }

        public static void PlaySound(string soundId)
        {
            if (instance == null)
                CreateInstance.Create(nameof(AudioManager));
            instance.ThisPlaySound(soundId);
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void OnDestroy()
        { 
            instance = null;
        }
        
        private void ThisPlayBGM(string bgmId)
        {
            Debug.Log("Play bgm");
        }
        
        private void ThisPlaySound(string soudId)
        {
            Debug.Log("Play sound");
        }
    }
}
