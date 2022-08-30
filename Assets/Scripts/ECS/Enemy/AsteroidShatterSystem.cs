using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

sealed class AsteroidShatterSystem : IEcsRunSystem
{
    private readonly EcsFilter<ShaterableComponent, TransformComponent, IsDeathTag> _shaterringFilter = null;

    private EcsWorld _world = null;
    private ObjectFactoryWithPool<AsteroidEcs> _smallAsteroidFactory;
    private GameData _gameData = null;

    public AsteroidShatterSystem(ObjectFactoryWithPool<AsteroidEcs> smallAsteroidFactory)
    {
        _smallAsteroidFactory = smallAsteroidFactory;
    }

    public void Run()
    {
        if (_shaterringFilter.IsEmpty()) return;

        foreach (var i in _shaterringFilter)
        {
            ref var shatter = ref _shaterringFilter.Get1(i).ShatterPositions;

            int smallCount = Random.Range(0, shatter.Count);

            Transform trans = _smallAsteroidFactory.Spawn().transform;

            List<Transform> copyShatter = new List<Transform>();

            foreach(var point in shatter)
            {
                copyShatter.Add(point);
            }

            ref var origin = ref _shaterringFilter.Get2(i).MyTransfrom;

            for (int j = 0; j < smallCount; j++)
            {
                int randomIndex = Random.Range(0, copyShatter.Count);

                trans.position = copyShatter[randomIndex].position;

                var entity = _world.NewEntity();
                entity.Get<DirectionComponent>().Direction = copyShatter[randomIndex].position - origin.position;
                entity.Get<MoveSpeedComponent>().Speed = _gameData.SmallAsteroidSpeed;

                copyShatter.RemoveAt(randomIndex);
            }
        }
    }
}
