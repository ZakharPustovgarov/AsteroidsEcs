using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Gun _gun;

    protected override void AddComponents()
    {
        base.AddComponents();

        _entity.Get<PlayerTag>();
        _gun.Construct(_entity);
    }
}
