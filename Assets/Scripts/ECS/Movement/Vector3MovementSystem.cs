using Leopotam.Ecs;
using UnityEngine;

sealed class Vector3MovementSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformComponent, DirectionComponent, MoveSpeedComponent>.Exclude<IsDeathTag> _moveFilter = null;
    private readonly EcsFilter<TransformComponent, ConstantDirection, MoveSpeedComponent>.Exclude<IsDeathTag> _constantMoveFilter = null;

    public void Run()
    {
        if (_moveFilter.IsEmpty() && _constantMoveFilter.IsEmpty()) return;

        foreach(var i in _moveFilter)
        {
            ref var trans = ref _moveFilter.Get1(i).MyTransfrom;
            ref var dir = ref _moveFilter.Get2(i).Direction;
            ref var speed = ref _moveFilter.Get3(i).Speed;

            trans.position += dir * speed * Time.deltaTime;
        }

        foreach (var i in _constantMoveFilter)
        {
            ref var trans = ref _constantMoveFilter.Get1(i).MyTransfrom;
            ref var dir = ref _constantMoveFilter.Get2(i).Direction;
            ref var speed = ref _constantMoveFilter.Get3(i).Speed;

            trans.position += dir * speed * Time.deltaTime;
        }
    }
}

