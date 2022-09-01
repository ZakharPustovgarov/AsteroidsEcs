using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEcs : EnemyEcs
{
    [SerializeField] private bool _isBig;
    [SerializeField] private List<Transform> _shatterPoints;

    protected override void AddComponents()
    {
        base.AddComponents();

        _entity.Get<AsteroidTag>();

        if(_isBig) _entity.Get<ShaterableComponent>().ShatterPositions = _shatterPoints;
    }
}
