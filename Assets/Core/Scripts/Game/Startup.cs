using Leopotam.EcsLite;
using UnityEngine;
using AB_Utility.FromSceneToEntityConverter;
using Game;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using LGrid;
using LSound;
using PoolSystem.Alternative;
using SevenBoldPencil.EasyEvents;
using UI;

namespace Client {
    public sealed class Startup : MonoBehaviour 
    {
        private EcsWorld _world;        
        private IEcsSystems _gameSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _initSystems;
        private EventsBus _eventsBus;

        private void Start () 
        {
            _eventsBus = new EventsBus();

            _world = new EcsWorld ();
            _initSystems = new EcsSystems(_world);
            _gameSystems = new EcsSystems (_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            
            AddInitSystems();
            AddRunSystems();
            AddEditorSystems();

            InjectAllSystems(_initSystems, _gameSystems, _fixedUpdateSystems);
            AddEventsDestroyer();
            
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _gameSystems.ConvertScene().Init();
        }

        private void Update () 
        {
            _gameSystems?.Run ();
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
                .Add(new InitUIInterface())
                .Add(new InitUIButtons())
                ;
        }
        
        private void AddRunSystems() 
        {
            _gameSystems
                .Add(new ClicksSystem())
                .Add(new MousePositionConvertSystem())
                
                .Add(new SwitchTurnSystem());
            
            if (Application.platform != RuntimePlatform.Android)
            {
                _gameSystems
                    .Add(new GhostOverGridSystem())
                    .Add(new SwitchGhostSystem());
            }
            
            _gameSystems
                .Add(new SetupChipSystem())
                .Add(new SetupMarkLimiterSystem())
                
                .Add(new MakeMarkTransparentSystem())
                .Add(new MarkFadeInSystem())
                
                .Add(new CheckForWinSystem())
                .Add(new SetupMarkLimiterCleanSystem())
                .Add(new WinSystem())
                .Add(new ResetTableSystem())
                
                .Add(new MarkFadeOutSystem())
                .Add(new SoundBridgeSystem())
                .Add(new MusicBridgeSystem())
                .Add(new SoundSystem())
                .Add(new MusicSystem())
                .Add(new CurrentMarkTurnShowUISystem())
                .Add(new ScoreSystem())
                
                .DelHere<CMousePosition>()
                .DelHere<EClearScoreButtonClicked>()
                .DelHere<EInfoButtonClicked>()
                .DelHere<EInfoButtonCloseClicked>()
                .DelHere<EMusicButtonClicked>()
                .DelHere<ESFXButtonClicked>()
                ;
        }

        private void OnDestroy () 
        {
            _gameSystems?.Destroy ();
            _gameSystems = null;

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
            _gameSystems
                .Add(_eventsBus.GetDestroyEventsSystem()
                    .IncReplicant<ESetup>()
                    .IncReplicant<EResetTable>()
                    .IncReplicant<EMouseOnGrid>()
                    .IncReplicant<EOnMarkSetup>()
                    .IncReplicant<EOnTurnSwitched>()
                    .IncReplicant<PlayMusic>()
                    
                    .IncSingleton<EWin>()
                    .IncSingleton<ELeftMouseClicked>()
                
                    .IncReplicant<EFadeOutMarks>()
                )
                ;
        }

        private void InjectAllSystems(params IEcsSystems[] systems)
        {
            var map = new Map();
            var poolService = new PoolService("Pools");
            foreach (var system in systems)
            {
                system.Inject(_eventsBus)
                      .Inject(map)
                      .Inject(poolService);
            }
        }
    }
}