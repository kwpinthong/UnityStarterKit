using UnityEngine;
using UnityEngine.Serialization;

namespace StarterKit.AudioManagerLib
{
    [System.Serializable]
    public class AudioData
    {
        public string Id; 
        [Range(0, 1)]
        public float PlayVoume = 1f;
        public AudioClip[] AudioClips;

        public AudioClip GetClip(int index = 0)
        {
            int length = AudioClips.Length;
            if (length == 0) return null;
            if (length == 1) return AudioClips[0];
            return AudioClips[Random.Range(0, length)];
        }
    }
}
