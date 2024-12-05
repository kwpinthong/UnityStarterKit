using System.Collections.Generic;
using DG.Tweening;
using StarterKit.Common;
using UnityEngine;
using UnityEngine.Audio;

namespace StarterKit.AudioManagerLib
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        
        public static void PlayBGM(string bgmId)
        {
            EnsureCreateInstance();
            instance.ThisPlayBGM(bgmId);
        }

        public static void PlaySound(string soundId)
        {
            EnsureCreateInstance();
            instance.ThisPlaySound(soundId);
        }
        
        public static void SetBGMVolume(float volume) => SetVolume(instance.adioMixerBGMParmName, volume);

        public static void SetSoundVolume(float volume) => SetVolume(instance.adioMixerSFXParmName, volume);

        public static void MuteBGM(bool isMute) => SetVolume(instance.adioMixerBGMParmName, isMute ? 0f : instance.BGMVolume);

        public static void MuteSound(bool isMute) => SetVolume(instance.adioMixerSFXParmName, isMute ? 0f : instance.SFXVolume);
        
        private static void SetVolume(string paramName, float volumeLevel)
        {
            EnsureCreateInstance();
            
            var finalVolume = Mathf.Clamp(volumeLevel, 0.0001f, 1f);
            var volume = Mathf.Log10(finalVolume) * 20;
            
            instance.audioMixer.SetFloat(paramName, volume);
        }
        
        private static void EnsureCreateInstance()
        {
            if (instance == null)
                CreateInstance.Create(nameof(AudioManager));
        }
        
        [SerializeField] 
        private AudioBank audioBank;
        [SerializeField]
        private AudioSource bgmSource;
        [SerializeField]
        private AudioSource sfxSource;
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private string adioMixerBGMParmName = "BGM";
        [SerializeField]
        private string adioMixerSFXParmName = "SFX";
        
        [Header("BGM Setting")]
        [SerializeField]
        private float bgmCrossFadeDuration = 1f;

        private AudioSource bgmSource1;
        private AudioSource bgmSource2;
        private List<AudioSource> sfxSources = new List<AudioSource>();
        
        private float BGMVolume => PlayerPrefs.GetFloat("StarterKit/AudioManager/BGMVolume", 1f);
        private float SFXVolume => PlayerPrefs.GetFloat("StarterKit/AudioManager/SFXVolume", 1f);

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
            var audioData = audioBank.GetAudioData(bgmId);

            if (audioData == null)
            {
                CommonDebug.LogWarning($"audio.Id = {bgmId} not found");
                return;
            }

            var audioClip = audioData.GetClip();
            if (audioClip == null)
            { 
                CommonDebug.LogWarning($"audio.Id = {bgmId} has no clip");
                return;
            }

            if (bgmSource1 == null && bgmSource2 == null)
            {
                bgmSource1 = Instantiate(bgmSource, transform);
                bgmSource2 = Instantiate(bgmSource, transform);
            }
            
            var nextSource = bgmSource1.isPlaying ? bgmSource2 : bgmSource1;
            var currentSource = bgmSource1.isPlaying ? bgmSource1 : bgmSource2;
            
            nextSource.volume = 0f;
            nextSource.clip = audioClip;
            nextSource.Play();
            
            CrossFade(currentSource, audioClip, currentSource.volume, 0f, bgmCrossFadeDuration);
            CrossFade(nextSource, audioClip, 0f, audioData.PlayVoume, bgmCrossFadeDuration);
            
            void CrossFade(AudioSource source, AudioClip clip, float startVolume, float volume, float duration)
            {
                source.clip = clip;
                source.volume = startVolume;
                source.Play();
                DOTween.To(() => source.volume, x => source.volume = x, volume, duration);
            }
        }
        
        private void ThisPlaySound(string soudId)
        {
            var audioData = audioBank.GetAudioData(soudId);

            if (audioData == null)
            {
                CommonDebug.LogWarning($"audio.Id = {soudId} not found");
                return;
            }

            var audioClip = audioData.GetClip();
            if (audioClip == null)
            { 
                CommonDebug.LogWarning($"audio.Id = {soudId} has no clip");
                return;
            }

            var audioSource = sfxSources.Find(x => !x.isPlaying);
            if (audioSource == null)
            {
                audioSource = Instantiate(sfxSource, transform);
                sfxSources.Add(audioSource);
            }
            
            audioSource.clip = audioClip;
            audioSource.volume = audioData.PlayVoume;
            audioSource.Play();
        }
    }
}
