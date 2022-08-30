using Leopotam.Ecs;

sealed class ScoreAddingCalculationSystem : IEcsRunSystem
{
    private readonly EcsFilter<IsDeathTag> _deadFilter = null;

    private GameData _gameData;
    private EcsWorld _world = null;

    public void Run()
    {
        if (_deadFilter.IsEmpty()) return;
        
        foreach(var i in _deadFilter)
        {
            ref var entity = ref _deadFilter.GetEntity(i);

            if (entity.Has<UfoTag>()) _world.NewEntity().Get<ScoreAddedComponent>().Score = _gameData.ScoreForUFO;

            if(entity.Has<AsteroidTag>())
            {
                if(entity.Has<ShaterableComponent>()) _world.NewEntity().Get<ScoreAddedComponent>().Score = _gameData.ScoreForBigAsteroid;
                else _world.NewEntity().Get<ScoreAddedComponent>().Score = _gameData.ScoreForSmall;
            }
        }
    }
}
