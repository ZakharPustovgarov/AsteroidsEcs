using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DeathBringer))]
public class Character : MonoBehaviour
{
    protected EcsWorld _world;
    protected EcsEntity _entity;
    [SerializeField] protected Health _health;
    [SerializeField] protected DeathBringer _deathBringer;

    protected virtual void Start()
    {
        _world = WorldHandler.GetWorld();

        AddComponents();
    }

    protected virtual void AddComponents()
    {
        _entity = _world.NewEntity();

        _entity.Get<TransformComponent>().MyTransfrom = transform;

        _health.Construct(_entity);
        _deathBringer.Construct(_entity);
    }
}
