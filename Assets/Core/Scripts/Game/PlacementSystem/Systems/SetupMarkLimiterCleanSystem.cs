using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;

namespace Game
{
    public class SetupMarkLimiterCleanSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CMarkLimiter>> _cMarkLimiterFilter;
        
        public void Run(IEcsSystems systems)
        {
            if (_bus.Value.HasEventSingleton<EWin>())
            {
                foreach (var entity in _cMarkLimiterFilter.Value)
                {
                    ref var limiters = ref _cMarkLimiterFilter.Pools.Inc1.Get(entity);
                    ClearQueues(ref limiters);
                }
            }
        }

        private void ClearQueues(ref CMarkLimiter limiters)
        {
            limiters.XMarks.Clear();
            limiters.OMarks.Clear();
        }
    }
}