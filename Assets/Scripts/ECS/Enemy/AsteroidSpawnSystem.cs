using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

sealed class AsteroidSpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private readonly EcsFilter<AsteroidSpawnCountEndEvent> _spawnFilter = null;
    private readonly EcsFilter<SpawnPointTag, TransformComponent> _spawnPointsFilter = null;

    private EcsWorld _world = null;
    private GameData _gameData = null;
    private ObjectFactoryWithPool<AsteroidEcs> _asteroidFactory = null;

    public AsteroidSpawnSystem(ObjectFactoryWithPool<AsteroidEcs> asteroidFactory)
    {
        _asteroidFactory = asteroidFactory;
    }

    public void Init()
    {
        _world.NewEntity().Get<AsteroidSpawnCounterComponent>().Duration = _gameData.AsteroidSpawnTime;
    }

    public void Run()
    {
        if (_spawnFilter.IsEmpty() || _spawnPointsFilter.IsEmpty()) return;

        List<Transform> spawnPoints = new List<Transform>();

        foreach (var i in _spawnPointsFilter)
        {
            spawnPoints.Add(_spawnPointsFilter.Get2(i).MyTransfrom);
        }

        foreach (var i in _spawnFilter)
        {
            AsteroidEcs asteroid = _asteroidFactory.Spawn();
            Transform trans = asteroid.transform;

            int randomIndex = Random.Range(0, spawnPoints.Count);

            int randomDirection = Random.Range(0, spawnPoints.Count);

            while(randomDirection == randomIndex)
            {
                randomDirection = Random.Range(0, spawnPoints.Count);
            }

            trans.position = spawnPoints[randomIndex].position;

            asteroid.Initialize();
            var entity = asteroid.GetEntity();
            entity.Get<ConstantDirection>().Direction = Vector3.Normalize(spawnPoints[randomDirection].position - spawnPoints[randomIndex].position);
            entity.Get<MoveSpeedComponent>().Speed = _gameData.BigAsteroidSpeed;

            spawnPoints.RemoveAt(randomIndex);

            _world.NewEntity().Get<AsteroidSpawnCounterComponent>().Duration = _gameData.AsteroidSpawnTime;
        }
    }
}
