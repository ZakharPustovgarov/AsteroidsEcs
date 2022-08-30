using Leopotam.Ecs;
using UnityEngine;

sealed class RotateSystem : IEcsRunSystem
{
    private readonly EcsFilter<RotateComponent, TransformComponent> _rotateFilter;

    private GameData _gameData = null;

    public void Run()
    {
        if(_rotateFilter.IsEmpty())
        {
            return;
        }

        foreach(var i in _rotateFilter)
        {
            ref var trans = ref _rotateFilter.Get2(i).MyTransfrom;
            ref var rotate = ref _rotateFilter.Get1(i).RotateType;

            if(rotate == RotateType.LEFT)
            {
                trans.Rotate(0, 0, _gameData.RotateSpeed * Time.deltaTime);
            }
            else if(rotate == RotateType.RIGHT)
            {
                trans.Rotate(0, 0, -_gameData.RotateSpeed * Time.deltaTime);
            }
        }
    }
}
