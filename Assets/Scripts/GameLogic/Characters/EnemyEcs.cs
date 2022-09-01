using Leopotam.Ecs;
using UnityEngine;

public class EnemyEcs : Character
{
    [SerializeField] protected DamagingObject _damageObj;

    protected override void Start()
    {
        
    }

    public void Initialize()
    {
        AddComponents();
    }

    protected override void AddComponents()
    {
        base.AddComponents();
        _damageObj.Spawn();

        _deathBringer.DiedEvent += _damageObj.Despawn;
    }

    public EcsEntity GetEntity()
    {
        return _entity;
    }
}
