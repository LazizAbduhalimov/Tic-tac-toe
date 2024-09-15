using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LSound;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class SoundBridgeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsPool<EOnMarkSetup> _eMarkSetup;

        private AllSounds _allSounds;
        
        public void Init(IEcsSystems systems)
        {
            _allSounds = Object.FindObjectOfType<AllSounds>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eMarkSetup))
            {
                ref var setup = ref _eMarkSetup.Get(entity);
                var clip = setup.MarkType switch
                {
                    Marks.X => _allSounds.SetupX.GetRandomElement(),
                    Marks.O => _allSounds.SetupO.GetRandomElement(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                _bus.Value.NewEvent(new PlaySound(clip, _allSounds.SfxMixerGroup, Vector3.zero));
            }
        }
    }
}