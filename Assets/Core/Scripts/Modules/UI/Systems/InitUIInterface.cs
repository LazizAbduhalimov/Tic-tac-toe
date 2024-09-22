using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace UI
{
    public class InitUIInterface : IEcsInitSystem
    {
        private EcsPoolInject<CInterface> _cInterface;
            
        public void Init(IEcsSystems systems)
        {
            ref var cInterface = ref _cInterface.NewEntity(out _);
            var ui = Object.FindObjectOfType<UILinks>();
            cInterface.InfoButton = ui.InfoButton;
            cInterface.MusicButton = ui.MusicButton;
            cInterface.SfxButton = ui.SfxButton;
            cInterface.ClearScoreButton = ui.ClearScoreButton;
            cInterface.CloseInfoButton = ui.CloseInfoButton;
        }
    }
}