using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PoolSystem.Alternative;
using PrimeTween;
using SevenBoldPencil.EasyEvents;
using Object = UnityEngine.Object;

namespace LSound
{
    public class SoundSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsCustomInject<PoolService> _poolService;
        private PoolMono<SoundSourceObject> _audioSourcePool;
        private EcsPool<PlaySound> _playSoundPool;

        public void Init(IEcsSystems systems)
        {
            var sound = Object.FindObjectOfType<SoundRefs>();
            _audioSourcePool = _poolService.Value.GetOrRegisterPool(sound.SoundSourceObject, 10);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _playSoundPool))
            {
                ref var sound = ref _playSoundPool.Get(entity);
                var audioSource = _audioSourcePool.GetFreeElement();
                LocateAudioSource(audioSource, in sound);
                audioSource.AudioSource.outputAudioMixerGroup = sound.MixerGroup;
                audioSource.PlayClip(sound.Clip);
                _playSoundPool.Del(entity);
            }
        }

        private void LocateAudioSource(SoundSourceObject source, in PlaySound sound)
        {
            var transform = source.transform;
            transform.position = sound.Position;
            if (sound.Parent == null) return;
            var parent = source.transform.parent;
            transform.parent = sound.Parent;
            Tween.Delay(sound.Clip.length, () => transform.parent = parent);
        }
    }
}
