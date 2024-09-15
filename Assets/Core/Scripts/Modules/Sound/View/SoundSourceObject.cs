using PoolSystem.Alternative;
using PrimeTween;
using UnityEngine;

namespace LSound
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundSourceObject : PoolObject
    {
        public AudioSource AudioSource;

        public void PlayClip(AudioClip clip, bool inactivateAfterPlay = true)
        {
            AudioSource.clip = clip;
            AudioSource.Play();

            if (inactivateAfterPlay)
                Tween.Delay(clip.length, () => gameObject.SetActive(false));
        }
    }
}
