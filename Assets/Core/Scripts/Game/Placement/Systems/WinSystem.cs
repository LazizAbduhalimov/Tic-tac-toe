using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LSound;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class WinSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsCustomInject<AllSounds> _allSounds;
        
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