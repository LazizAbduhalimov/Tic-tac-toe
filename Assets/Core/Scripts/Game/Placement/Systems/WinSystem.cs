using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class WinSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        
        public void Run(IEcsSystems systems)
        {
            if (_bus.Value.HasEventSingleton<EWin>(out var win))
            {
                Debug.Log($"{win.WonMark} Win!");
                _bus.Value.NewEvent<EResetTable>();
            }
        }
    }
}