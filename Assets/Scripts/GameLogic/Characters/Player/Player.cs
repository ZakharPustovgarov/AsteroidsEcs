using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    protected override void AddComponents()
    {
        base.AddComponents();

        _entity.Get<PlayerTag>();

    }
}
