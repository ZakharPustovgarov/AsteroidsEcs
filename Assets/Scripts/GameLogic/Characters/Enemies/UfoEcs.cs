using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;

public class UfoEcs : Character
{
    protected override void AddComponents()
    {
        base.AddComponents();

        _entity.Get<UfoTag>();
    }
}
