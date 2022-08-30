using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Voody.UniLeo;


public class EcsGameStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameData _gameData;

    private ObjectFactoryWithPool<UfoEcs> _ufoFactory;
    private ObjectFactoryWithPool<AsteroidEcs> _asteroidsBigFactory;
    private ObjectFactoryWithPool<AsteroidEcs> _asteroidsSmallFactory;
    private ObjectFactoryWithPool<Bullet> _bulletFactory;
    private ObjectFactoryWithPool<Laser> _laserFactory;

    private void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        InitializedFactories();

        _systems.ConvertScene();

        AddInjections();
        AddOneFrames();
        AddSystems();

        _systems.Init();

        WorldHandler.Init(_world);

    }

    private void InitializedFactories()
    {
        _ufoFactory = new ObjectFactoryWithPool<UfoEcs>(_gameData.UfoPrefab);
        _asteroidsBigFactory = new ObjectFactoryWithPool<AsteroidEcs>(_gameData.BigAsteroidPrefab);
        _asteroidsSmallFactory = new ObjectFactoryWithPool<AsteroidEcs>(_gameData.SmallAsteroidPrefab);
        _bulletFactory = new ObjectFactoryWithPool<Bullet>(_gameData.BulletPrefab);
        _laserFactory = new ObjectFactoryWithPool<Laser>(_gameData.LaserPrefab);
    }

    private void AddSystems()
    {
        _systems
            .Add(new CounterSystem())
            .Add(new AsteroidSpawnSystem())
            .Add(new UfoSpawnSystem())
            .Add(new PlayerKeyboardInputSystem())
            .Add(new EnemyTrackSystem())
            .Add(new SkinChangeSystem())
            .Add(new RotateSystem())
            .Add(new GunFireSystem())
            .Add(new BulletFlySystem())
            .Add(new BulletStopSystem())
            .Add(new LaserStopSystem())
            .Add(new Vector3MovementSystem())
            .Add(new DamageSystem())
            .Add(new ScoreAddingCalculationSystem())
            .Add(new AsteroidShatterSystem(_asteroidsSmallFactory))
            .Add(new DeathSystem())
            .Add(new ScoreSystem())
            ;
    }

    private void AddInjections()
    {
        _systems.Inject(_mainCamera);
        _systems.Inject(_gameData);

        _systems.Inject(_bulletFactory);
        _systems.Inject(_laserFactory);

        _systems.Inject(_ufoFactory);
        _systems.Inject(_asteroidsBigFactory);
    }

    private void AddOneFrames()
    {
        _systems
            .OneFrame<DirectionComponent>()
            ;
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
