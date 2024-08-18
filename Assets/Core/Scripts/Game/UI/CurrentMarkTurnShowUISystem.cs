using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using Object = UnityEngine.Object;

namespace Game
{
    public class CurrentMarkTurnShowUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CTurn>> _cTurnFilter;
        private EcsFilterInject<Inc<CMarkTurnUI>> _cMarkTurnUiFilter;
        private EcsPoolInject<CMarkTurnUI> _cMarkTurnUiPool;
        
        public void Init(IEcsSystems systems)
        {
            var markTurnShower = Object.FindObjectOfType<MarkTurnUIShower>();
            _cMarkTurnUiPool.NewEntity(out _).MarkTurnUIShower = markTurnShower;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _bus.Value.GetEventBodies<EOnTurnSwitched>(out _))
            {
                foreach (var entity in _cTurnFilter.Value)
                {
                    ref var turn = ref _cTurnFilter.Pools.Inc1.Get(entity);
                    SwitchMarkUI(turn.MarksTurn);
                }
            }
        }

        private void SwitchMarkUI(Marks turnMarksTurn)
        {
            foreach (var entity in _cMarkTurnUiFilter.Value)
            {
                ref var markTurnUi = ref _cMarkTurnUiPool.Value.Get(entity);
                markTurnUi.MarkTurnUIShower.SwitchMark(turnMarksTurn);
            }
        }
    }
}