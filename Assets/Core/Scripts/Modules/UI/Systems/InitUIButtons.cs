using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace UI
{
    public class InitUIButtons : IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<CInterface>> _cInterfaceFilter;

        private EcsPoolInject<CClearScoreButton> _cClearScoreButton;
        private EcsPoolInject<CInfoButton> _cInfoButton;
        private EcsPoolInject<CInfoCloseButton> _cInfoCloseButton;
        private EcsPoolInject<CMusicButton> _cMusicButton;
        private EcsPoolInject<CSFXButton> _cSfxButtonButton;
        
        
        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _cInterfaceFilter.Value)
            {
                ref var ui = ref _cInterfaceFilter.Pools.Inc1.Get(entity);

                UIUtils.InitButton(ui.InfoButton, _cInfoButton.Value);
                UIUtils.InitButton(ui.MusicButton, _cMusicButton.Value);
                UIUtils.InitButton(ui.SfxButton, _cSfxButtonButton.Value);
                UIUtils.InitButton(ui.ClearScoreButton, _cClearScoreButton.Value);
                UIUtils.InitButton(ui.CloseInfoButton, _cInfoCloseButton.Value);
            }
        }
    }
}