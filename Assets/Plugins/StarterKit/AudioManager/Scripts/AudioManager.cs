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
        
        public static float MasterVolume
        {
            get => PlayerPrefs.GetFloat(instance.GetPath(MasterVolumeKey), 1f);
            set => PlayerPrefs.SetFloat(instance.GetPath(MasterVolumeKey), value);
        }

        public static float BGMVolume
        {
            get => PlayerPrefs.GetFloat(instance.GetPath(BGMVolumeKey), 1f);
            set => PlayerPrefs.SetFloat(instance.GetPath(BGMVolumeKey), value);
        }
        
        public static float SFXVolume
        {
            get => PlayerPrefs.GetFloat(instance.GetPath(SFXVolumeKey), 1f);
            set => PlayerPrefs.SetFloat(instance.GetPath(SFXVolumeKey), value);
        }
        
        public static bool DoBGMMute
        {
            get => PlayerPrefs.GetInt(instance.GetPath(DoBGMMuteKey), 0) == 1;
            set => PlayerPrefs.SetInt(instance.GetPath(DoBGMMuteKey), value ? 1 : 0);
        }
        
        public static bool DoSFXMute
        {
            get => PlayerPrefs.GetInt(instance.GetPath(DoSFXMuteKey), 0) == 1;
            set => PlayerPrefs.SetInt(instance.GetPath(DoSFXMuteKey), value ? 1 : 0);
        }
        
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
        
        public static void SetMasterVolume(float volume)
        { 
            MasterVolume = volume;
            SetVolume(instance.adioMixerMasterParmName, volume);
        }

        public static void SetBGMVolume(float volume)
        { 
            BGMVolume = volume;
            SetVolume(instance.adioMixerBGMParmName, volume);
        }

        public static void SetSFXVolume(float volume)
        { 
            SFXVolume = volume;
            SetVolume(instance.adioMixerSFXParmName, volume);
        }

        public static void MuteBGM(bool isMute)
        { 
            DoBGMMute = isMute;
            SetVolume(instance.adioMixerBGMParmName, isMute ? 0f : BGMVolume);
        }

        public static void MuteSound(bool isMute)
        { 
            DoSFXMute = isMute;
            SetVolume(instance.adioMixerSFXParmName, isMute ? 0f : SFXVolume);
        }
        
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
        private bool dontDestroyOnLoad = true;
        [SerializeField] 
        private AudioBank audioBank;
        [SerializeField]
        private AudioSource bgmSource;
        [SerializeField]
        private AudioSource sfxSource;
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private string adioMixerMasterParmName = "Master";
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

        private const string PlayerPrefsPath = "StarterKit/AudioManager";
        private const string MasterVolumeKey = "MasterVolume";
        private const string BGMVolumeKey = "BGMVolume";
        private const string SFXVolumeKey = "SFXVolume";
        private const string DoBGMMuteKey = "DoBGMMute";
        private const string DoSFXMuteKey = "DoSFXMute";

        private string GetPath(string key) => $"{PlayerPrefsPath}/{key}";
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            if (dontDestroyOnLoad)
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
                source.loop = true;
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
