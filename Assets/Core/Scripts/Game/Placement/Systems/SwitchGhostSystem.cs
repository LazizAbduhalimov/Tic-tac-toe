using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;

namespace Game
{
    public class SwitchGhostSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CTurn>> _cTurnFilter;
        private EcsFilterInject<Inc<CGhost>> _cGhostFilter;
        
        public void Run(IEcsSystems systems)
        {
            if (!_bus.Value.HasEvents<EOnTurnSwitched>()) return;
            
            foreach (var entity in _cTurnFilter.Value)
            {
                ref var turn = ref _cTurnFilter.Pools.Inc1.Get(entity);
                foreach (var ghostEntity in _cGhostFilter.Value)
                {
                    ref var ghost = ref _cGhostFilter.Pools.Inc1.Get(ghostEntity);
                    switch (turn.MarksTurn)
                    {
                        case Marks.X:
                            ghost.XPrefab.gameObject.SetActive(true);
                            ghost.OPrefab.gameObject.SetActive(false);
                            break;
                        case Marks.O:
                            ghost.XPrefab.gameObject.SetActive(false);
                            ghost.OPrefab.gameObject.SetActive(true);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }    
        }
    }
}