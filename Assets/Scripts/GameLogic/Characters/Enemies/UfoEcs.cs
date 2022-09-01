using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;

public class UfoEcs : EnemyEcs
{
    protected override void AddComponents()
    {
        base.AddComponents();

        _entity.Get<UfoTag>();
    }
}
