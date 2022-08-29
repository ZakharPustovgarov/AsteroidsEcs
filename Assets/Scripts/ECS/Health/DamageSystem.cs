using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

sealed class DamageSystem : IEcsRunSystem
{
    private readonly EcsFilter<IDComponent, HealthComponent>.Exclude<IsDeathTag> _entityHealthFilter = null;
    private readonly EcsFilter<IDComponent, DamageComponent> _damageFilter = null;


    public void Run()
    {
        if (_entityHealthFilter.IsEmpty() || _damageFilter.IsEmpty()) return;

        foreach (var i in _damageFilter)
        {
            ref var id1 = ref _damageFilter.Get1(i);
            ref var damage = ref _damageFilter.Get2(i);

            foreach (var j in _entityHealthFilter)
            {
                ref var id2 = ref _entityHealthFilter.Get1(j);
                ref var health = ref _entityHealthFilter.Get2(j);

                if (id1.ID == id2.ID)
                {
                   health.CurrentHealth -= damage.Damage;

                    ref var entity = ref _entityHealthFilter.GetEntity(j);

                    if (health.CurrentHealth <= 0)
                    {
                        entity.Get<IsDeathTag>();
                    }
                    _damageFilter.GetEntity(i).Destroy();
                    break;
                }
            }
        }
    }
}
