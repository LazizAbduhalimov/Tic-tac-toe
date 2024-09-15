using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class ScoreSystem :IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;

        private ScoreMB _scoreMb;
        
        public void Init(IEcsSystems systems)
        {
            _scoreMb = Object.FindObjectOfType<ScoreMB>();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (_bus.Value.HasEventSingleton<EWin>(out var win))
            {
                _scoreMb.AddScore(win.WonMark);
            }
        }
    }
}