using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game
{
    public class TurnInitSystem : IEcsInitSystem
    {
        private EcsPoolInject<CTurn> _cTurnPool;
            
        public void Init(IEcsSystems systems)
        {
            _cTurnPool.NewEntity(out _).Invoke(Marks.X);
        }
    }
}