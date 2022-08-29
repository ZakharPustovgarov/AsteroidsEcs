using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class LerpMoveSystem : IEcsRunSystem
{
    //private readonly EcsFilter<TransformComponent, DirectionComponent, RotatableTag> _rotatableFilter = null;
    private readonly EcsFilter<TransformComponent, TimeLerpComponent, Vector3LerpComponent, MovableTag> _lerpMoveFilter = null;

    public void Run()
    {
        if (_lerpMoveFilter.IsEmpty()) return;

        foreach (var i in _lerpMoveFilter)
        {
            ref var transformComponent = ref _lerpMoveFilter.Get1(i);
            ref var lerpComponent = ref _lerpMoveFilter.Get2(i);
            ref var vectorComponent = ref _lerpMoveFilter.Get3(i);

            ref var transform = ref transformComponent.MyTransfrom;

            if (lerpComponent.PassedTime > lerpComponent.FullTime)
            {
                ref var entity = ref _lerpMoveFilter.GetEntity(i);
                entity.Del<TimeLerpComponent>();
                entity.Del<Vector3LerpComponent>();
                entity.Del<MovableTag>();
                continue;
            }

            lerpComponent.PassedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(vectorComponent.StartVector, vectorComponent.EndVector, lerpComponent.Curve.Evaluate( lerpComponent.PassedTime / lerpComponent.FullTime));
        }
    }
}
