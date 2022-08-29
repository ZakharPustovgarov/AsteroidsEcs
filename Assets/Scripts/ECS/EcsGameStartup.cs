using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Voody.UniLeo;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EcsGameStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    //[SerializeField] private BulletPool _bulletPool;
    //[SerializeField] private GameStaticData _staticData;
    //[SerializeField] private SceneData _sceneData;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameData _gameData;

    private void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.ConvertScene();

        AddInjections();
        AddOneFrames();
        AddSystems();

        _systems.Init();

        WorldHandler.Init(_world);

    }

    private void AddSystems()
    {
        _systems
            .Add(new PlayerInputSystem())
            ;
    }

    private void AddInjections()
    {
        _systems.Inject(_mainCamera);
        _systems.Inject(_gameData);
    }

    private void AddOneFrames()
    {
        //_systems.OneFrame<TurnEndEvent>();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        if (_systems == null) return;

        _systems.Destroy();
        _systems = null;
        _world.Destroy();
        _world = null;
    }
}
