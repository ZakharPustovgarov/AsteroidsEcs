using Leopotam.Ecs;

sealed class RotateSystem : IEcsRunSystem
{
    private readonly EcsFilter<RotateComponent, TransformComponent> _rotateFilter;

    private GameData _gameData = null;

    public void Run()
    {
        throw new System.NotImplementedException();
    }
}
