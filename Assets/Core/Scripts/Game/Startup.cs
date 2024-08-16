using Leopotam.EcsLite;
using UnityEngine;
using AB_Utility.FromSceneToEntityConverter;
using Game;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using LGrid;
using PoolSystem.Alternative;
using SevenBoldPencil.EasyEvents;

namespace Client {
    public sealed class Startup : MonoBehaviour 
    {
        private EcsWorld _world;        
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _initSystems;
        private EventsBus _eventsBus;

        private void Start () 
        {
            _eventsBus = new EventsBus();

            _world = new EcsWorld ();
            _initSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems (_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            
            AddInitSystems();
            AddRunSystems();
            AddEditorSystems();

            InjectAllSystems(_initSystems, _updateSystems, _fixedUpdateSystems);
            AddEventsDestroyer();
            
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.ConvertScene().Init();
        }

        private void Update () 
        {
            _updateSystems?.Run ();
        }
        
        private void FixedUpdate() 
        {
            _fixedUpdateSystems?.Run();
        }

        private void AddInitSystems()
        {
            _initSystems
                .Add(new GridInitSystem())
                .Add(new TurnInitSystem())
                .Add(new InitPoolsSystem())
                ;
        }
        
        private void AddRunSystems() 
        {
            _updateSystems
                .Add(new ClicksSystem())
                .Add(new MousePositionConvertSystem())
                
                .Add(new SwitchTurnSystem())
                .Add(new SwitchGhostSystem())
                
                .Add(new GhostOverGridSystem())
                .Add(new SetupChipSystem())
                .Add(new SetupMarkLimiterSystem())
                
                .Add(new MakeMarkTransparentSystem())
                
                .Add(new CheckForWinSystem())
                .Add(new SetupMarkLimiterCleanSystem())
                .Add(new WinSystem())
                .Add(new ResetTableSystem())
                
                .DelHere<CMousePosition>()
                ;
        }

        private void OnDestroy () 
        {
            _updateSystems?.Destroy ();
            _updateSystems = null;

            _fixedUpdateSystems?.Destroy();
            _fixedUpdateSystems = null;

            _world?.Destroy ();
            _world = null;

            _eventsBus.Destroy();
        }

        private void AddEditorSystems() 
        {
            // #if UNITY_EDITOR
            //     _updateSystems
            //         .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
            // #endif
        }

        private void AddEventsDestroyer()
        {
            _updateSystems
                .Add(_eventsBus.GetDestroyEventsSystem()
                    .IncReplicant<ESetup>()
                    .IncReplicant<EResetTable>()
                    .IncReplicant<EOnMarkSetup>()
                    .IncReplicant<EOnTurnSwitched>()
                    
                    .IncSingleton<EWin>()
                    .IncSingleton<ELeftMouseClicked>()
                )
                ;
        }

        private void InjectAllSystems(params IEcsSystems[] systems)
        {
            var map = new Map();
            var poolService = new PoolService<PoolObject>("Pools");
            foreach (var system in systems)
            {
                system.Inject(_eventsBus)
                      .Inject(map)
                      .Inject(poolService);
            }
        }
    }
}