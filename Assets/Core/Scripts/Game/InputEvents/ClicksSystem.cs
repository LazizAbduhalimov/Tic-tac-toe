using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class ClicksSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        
        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0)) _bus.Value.NewEventSingleton<ELeftMouseClicked>();
        }
    }
}