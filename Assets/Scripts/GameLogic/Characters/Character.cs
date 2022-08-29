using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class Character : MonoBehaviour
{
    protected EcsWorld _world;
    protected EcsEntity _entity;

    protected virtual void Start()
    {
        _world = WorldHandler.GetWorld();

        AddComponents();
    }

    protected virtual void AddComponents()
    {
        _entity = _world.NewEntity();

        _entity.Get<TransformComponent>().MyTransfrom = transform;
    }
}
