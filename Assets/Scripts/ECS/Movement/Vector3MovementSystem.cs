using Leopotam.Ecs;
using UnityEngine;

sealed class Vector3MovementSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformComponent, DirectionComponent, MoveSpeedComponent> _moveFilter = null;

    public void Run()
    {
        if (_moveFilter.IsEmpty()) return;

        foreach(var i in _moveFilter)
        {
            ref var trans = ref _moveFilter.Get1(i).MyTransfrom;
            ref var dir = ref _moveFilter.Get2(i).Direction;
            ref var speed = ref _moveFilter.Get3(i).Speed;

            trans.position += dir * speed * Time.deltaTime;
        }
    }
}

