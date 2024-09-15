using SevenBoldPencil.EasyEvents;
using UnityEngine;
using UnityEngine.Audio;

namespace LSound
{
    public struct PlayMusic : IEventReplicant
    {
        public AudioClip Clip;
        public AudioMixerGroup MixerGroup;
        
        public PlayMusic(AudioClip clip, AudioMixerGroup mixerGroup)
        {
            Clip = clip;
            MixerGroup = mixerGroup;
        }
    }
}