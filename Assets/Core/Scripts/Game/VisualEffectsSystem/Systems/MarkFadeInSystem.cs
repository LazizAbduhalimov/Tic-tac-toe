using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PrimeTween;
using SevenBoldPencil.EasyEvents;
using UnityEngine;


namespace Game
{
    public class MarkFadeInSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsPool<EOnMarkSetup> _eOnMarkSetupPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eOnMarkSetupPool))
            {
                ref var setup = ref _eOnMarkSetupPool.Get(entity);
                FadeIn(setup.Mark);
            }    
        }

        private void FadeIn(GameObject setupMark)
        {
            var transform = setupMark.transform.GetChild(0);
            var position = transform.position;
            
            transform.localScale = Vector3.zero;
            transform.position += Vector3.up * 0.2f;

            Sequence.Create()
                .Chain(Tween.Scale(transform, 1f, 0.125f, Ease.InSine)
                .Chain(Tween.PositionY(transform, position.y, 0.125f, Ease.InSine)
            ));
        }
    }
}