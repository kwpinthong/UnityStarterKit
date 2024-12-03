using System.Collections.Generic;
using UnityEngine;

namespace StarterKit.AudioManagerLib
{
    [CreateAssetMenu(fileName = "AudioBank", menuName = "StarterKit/AudioManager/AudioBank")]
    public class AudioBank : ScriptableObject
    {
       [SerializeField]
       private List<AudioData> audioDatas = new List<AudioData>(); 
       
       public AudioData GetAudioData(string id) => audioDatas.Find(x => x.Id == id);
    }
}
