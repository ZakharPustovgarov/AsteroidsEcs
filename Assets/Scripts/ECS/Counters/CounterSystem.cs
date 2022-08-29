using Leopotam.Ecs;
using System.Diagnostics;
using UnityEngine;

sealed class CounterSystem : IEcsRunSystem
{
    private readonly EcsFilter<CounterComponent> _counterFilter = null;
    private readonly EcsFilter<RessurectCounterComponent> _ressurectFilter = null;

    public void Run()
    {
        if (_counterFilter.IsEmpty() && _ressurectFilter.IsEmpty()) return;

        foreach (var i in _counterFilter)
        {
            ref var block = ref _counterFilter.Get1(i);
            block.Duration -= Time.deltaTime;

            if (block.Duration <= 0)
            {
                //UnityEngine.Debug.Log("Count ended");
                ref var entity = ref _counterFilter.GetEntity(i);
                entity.Del<CounterComponent>();
                entity.Get<CountEndEvent>();
            }
        }

        foreach (var i in _ressurectFilter)
        {
            ref var block = ref _ressurectFilter.Get1(i);
            block.Duration -= Time.deltaTime;
            //UnityEngine.Debug.Log("Ressurect duartion: " + block.Duration);
            if (block.Duration <= 0)
            {
                
                ref var entity = ref _ressurectFilter.GetEntity(i);
                entity.Del<RessurectCounterComponent>();
                entity.Get<RessurectCountEndEvent>();
            }
        }
    }
}
