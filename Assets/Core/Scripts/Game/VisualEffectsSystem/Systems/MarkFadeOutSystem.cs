using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using PrimeTween;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class MarkFadeOutSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsPool<EFadeOutMarks> _eFadeOutMarks;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eFadeOutMarks))
            {
                ref var fadeOutMarks = ref _eFadeOutMarks.Get(entity);
                TweenWinRow(fadeOutMarks.WinRow);
                TweenOthers(fadeOutMarks.Others);
            }
        }

        private void TweenWinRow(MarkMb[] row)
        {
            foreach (var mark in row)
            {
                var transform = mark.transform.GetChild(0);
                Tween.Scale(transform, 0f, 0.5f, Ease.InOutBack, startDelay: 0.5f)
                    .OnComplete(() => {
                        mark.gameObject.SetActive(false);
                        mark.transform.localScale = Vector3.one;
                    });
            }
        }

        private void TweenOthers(MarkMb[] others)
        {
            foreach (var mark in others)
            {
                var transform = mark.transform.GetChild(0);
                Tween.Scale(transform, 0f, 0.5f)
                    .OnComplete(() => {
                        mark.gameObject.SetActive(false);
                        mark.transform.localScale = Vector3.one;
                    });
            }
        }
    }
}