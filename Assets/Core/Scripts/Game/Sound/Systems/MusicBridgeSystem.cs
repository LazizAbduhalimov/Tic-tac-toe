using System;
using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LSound;
using PrimeTween;
using SevenBoldPencil.EasyEvents;
using Object = UnityEngine.Object;

namespace Game
{
    public class MusicBridgeSystem : IEcsInitSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CMusic>, Exc<CPlayed>> _unPlayedMusics;
        private EcsFilterInject<Inc<CMusic, CPlayed>> _playedMusics;
        
        private EcsPoolInject<CMusic> _cMusic;
        private EcsPoolInject<CPlayed> _cPlayed;
        
        private EcsPool<PlayMusic> _playMusic;

        private AllSounds _allSounds;
        
        public void Init(IEcsSystems systems)
        {
            _allSounds = Object.FindObjectOfType<AllSounds>();
            var random = new Random();
            var musics = _allSounds.Musics.OrderBy(x => random.Next()).ToArray();
            foreach (var music in musics) _cMusic.NewEntity(out _).Clip = music;
            PlayRandomMusic();
        }

        private void PlayRandomMusic()
        {
            float musicLength = int.MaxValue;
            foreach (var entity in _unPlayedMusics.Value)
            {
                ref var music = ref _cMusic.Value.Get(entity);
                _bus.Value.NewEvent(new PlayMusic(music.Clip, _allSounds.MusicMixerGroup));
                musicLength = music.Clip.length;
                _cPlayed.Value.Add(entity);
                break;
            }

            if (_unPlayedMusics.Value.GetEntitiesCount() == 0)
            {
                foreach (var entity in _playedMusics.Value) _cPlayed.Value.Del(entity);
            }

            Tween.Delay(musicLength, PlayRandomMusic);
        }
    }
}