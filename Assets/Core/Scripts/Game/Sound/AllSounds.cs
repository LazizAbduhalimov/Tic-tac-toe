using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class AllSounds : MonoBehaviour
    {
        public AudioClip[] SetupX;
        public AudioClip[] SetupO;
        public AudioClip Win;
        
        [Space(5f)] 
        public AudioClip[] Musics;
        
        public AudioMixerGroup MusicMixerGroup;
        public AudioMixerGroup SfxMixerGroup;
    }
}