using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

sealed class UfoSpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private readonly EcsFilter<UfoSpawnCountEndEvent> _spawnFilter = null;
    private readonly EcsFilter<SpawnPointTag, TransformComponent> _spawnPointsFilter = null;

    private EcsWorld _world = null;
    private GameData _gameData = null;
    private ObjectFactoryWithPool<UfoEcs> _ufoFactory = null;

    public void Init()
    {
        _world.NewEntity().Get<UfoSpawnCounterComponent>().Duration = _gameData.UfoSpawnTime;
    }

    public void Run()
    {
        if (_spawnFilter.IsEmpty() || _spawnPointsFilter.IsEmpty()) return;

        List<Transform> spawnPoints = new List<Transform>();

        foreach(var i in _spawnPointsFilter)
        {
            spawnPoints.Add(_spawnPointsFilter.Get2(i).MyTransfrom);
        }

        foreach (var i in _spawnFilter)
        {
            UfoEcs ufo = _ufoFactory.Spawn();
            Transform trans = ufo.transform;
            ufo.Initialize();
            int randomIndex = Random.Range(0, spawnPoints.Count);
            trans.position = spawnPoints[randomIndex].position;
            spawnPoints.RemoveAt(randomIndex);

            _world.NewEntity().Get<UfoSpawnCounterComponent>().Duration = _gameData.UfoSpawnTime;
        }
    }
}