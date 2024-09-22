using UnityEngine;
using UnityEngine.Audio;

namespace Client
{
    public class SoundSwitcher : MonoBehaviour
    {
        public AudioMixerGroup MixerGroup;
        
        public void SwitchMusic()
        {
            SwitchMixerVolume("MusicVolume", -15);
        }

        public void SwitchSfx()
        {
            SwitchMixerVolume("SFXVolume", -10);
        }

        private void SwitchMixerVolume(string mixerName, float maxValue)
        {
            MixerGroup.audioMixer.GetFloat(mixerName, out var volume);
            var setVolume = volume == maxValue ? -80 : maxValue;
            MixerGroup.audioMixer.SetFloat(mixerName, setVolume);
            Debug.Log($"{mixerName}: {setVolume}");
        }
    }
}