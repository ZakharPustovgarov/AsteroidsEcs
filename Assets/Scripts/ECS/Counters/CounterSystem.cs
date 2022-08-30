﻿using Leopotam.Ecs;
using System.Diagnostics;
using UnityEngine;

sealed class CounterSystem : IEcsRunSystem
{
    private readonly EcsFilter<CounterComponent> _counterFilter = null;
    private readonly EcsFilter<UfoSpawnCounterComponent> _ufoSpawnFilter = null;
    private readonly EcsFilter<AsteroidSpawnCounterComponent> _asteroidSpawnFilter = null;

    public void Run()
    {
        if (_counterFilter.IsEmpty() && _ufoSpawnFilter.IsEmpty() && _asteroidSpawnFilter.IsEmpty()) return;

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

        foreach (var i in _ufoSpawnFilter)
        {
            ref var block = ref _ufoSpawnFilter.Get1(i);
            block.Duration -= Time.deltaTime;
            //UnityEngine.Debug.Log("Ressurect duartion: " + block.Duration);
            if (block.Duration <= 0)
            {
                
                ref var entity = ref _ufoSpawnFilter.GetEntity(i);
                entity.Del<UfoSpawnCounterComponent>();
                entity.Get<UfoSpawnCountEndEvent>();
            }
        }

        foreach (var i in _asteroidSpawnFilter)
        {
            ref var block = ref _asteroidSpawnFilter.Get1(i);
            block.Duration -= Time.deltaTime;
            //UnityEngine.Debug.Log("Ressurect duartion: " + block.Duration);
            if (block.Duration <= 0)
            {

                ref var entity = ref _asteroidSpawnFilter.GetEntity(i);
                entity.Del<AsteroidSpawnCounterComponent>();
                entity.Get<AsteroidSpawnCountEndEvent>();
            }
        }
    }
}
