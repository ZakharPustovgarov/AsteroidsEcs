using Leopotam.Ecs;
using UnityEngine;

sealed class Vector3MovementSystem : IEcsRunSystem 
{
    private readonly EcsFilter<TransformComponent, DirectionComponent, MoveSpeedComponent>.Exclude<IsDeathTag> _moveFilter = null;

    public void Run()
    {
        //Debug.Log(_moveFilter.GetEntitiesCount());
        if (_moveFilter.IsEmpty()) return;

        foreach(var i in _moveFilter)
        {
            ref var trans = ref _moveFilter.Get1(i).MyTransfrom;
            ref var dir = ref _moveFilter.Get2(i);
            ref var speed = ref _moveFilter.Get3(i).Speed;
            //Debug.Log("-------------------------------");
            //Debug.Log("Pos before: " + trans.position, trans);
            trans.position += dir.Direction * speed * Time.deltaTime;
            //Debug.Log("Pos after: " + trans.position, trans);
            //Debug.Log("-------------------------------");
            if (!dir.IsConstant)
            {
                _moveFilter.GetEntity(i).Del<DirectionComponent>();
            }
        }

    }
}

