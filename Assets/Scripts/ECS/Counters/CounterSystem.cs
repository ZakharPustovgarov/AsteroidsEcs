using Leopotam.Ecs;
using System.Diagnostics;
using UnityEngine;

sealed class CounterSystem : IEcsRunSystem
{
    private readonly EcsFilter<CounterComponent> _counterFilter = null;


    public void Run()
    {
        if (_counterFilter.IsEmpty()) return;

        foreach (var i in _counterFilter)
        {
            ref var block = ref _counterFilter.Get1(i);
            block.Duration -= Time.deltaTime;

            if (block.Duration <= 0)
            {
                ref var entity = ref _counterFilter.GetEntity(i);
                entity.Del<CounterComponent>();
                entity.Get<CountEndEvent>();
            }
        }

    }
}
