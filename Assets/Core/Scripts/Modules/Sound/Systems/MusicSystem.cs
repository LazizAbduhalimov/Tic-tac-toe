using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LSound;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class MusicSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CMusicSource>> _cMusicSource;
        
        private EcsPoolInject<CMusicSource> _cMusic;
        private EcsPool<PlayMusic> _playMusic;
        
        public void Init(IEcsSystems systems)
        {
            var sound = Object.FindObjectOfType<SoundRefs>();
            var source = Object.Instantiate(sound.SoundSourceObject.AudioSource);
            _cMusic.NewEntity(out _).AudioSource = source;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _playMusic))
            {
                ref var music = ref _playMusic.Get(entity);
                foreach (var sourceEntity in _cMusicSource.Value)
                {
                    ref var musicSource = ref _cMusicSource.Pools.Inc1.Get(sourceEntity);
                    musicSource.AudioSource.Stop();
                    musicSource.AudioSource.outputAudioMixerGroup = music.MixerGroup;
                    musicSource.AudioSource.clip = music.Clip;
                    musicSource.AudioSource.Play();
                }
            }  
        }
    }
}