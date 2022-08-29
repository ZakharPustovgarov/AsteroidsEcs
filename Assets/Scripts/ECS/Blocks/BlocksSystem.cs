using Leopotam.Ecs;
using UnityEngine;

sealed class BlocksSystem : IEcsRunSystem
{
    private readonly EcsFilter<GlobalCooldown> _globalCooldown = null;
    private readonly EcsFilter<HealBlock> _healBlocksFilter = null;
    private readonly EcsFilter<EnemyBehaviourBlock> _enemyBlocksFilter = null;

    public void Run()
    {
        if (_globalCooldown.IsEmpty() && _healBlocksFilter.IsEmpty() && _enemyBlocksFilter.IsEmpty()) return;

        foreach(var i in _globalCooldown)
        {
            ref var block = ref _globalCooldown.Get1(i);
            block.Duration -= Time.deltaTime;

            if (block.Duration <= 0) _globalCooldown.GetEntity(i).Del<GlobalCooldown>();
        }

        foreach (var i in _healBlocksFilter)
        {
            ref var block = ref _healBlocksFilter.Get1(i);
            block.Duration -= Time.deltaTime;

            if (block.Duration <= 0) _healBlocksFilter.GetEntity(i).Del<HealBlock>();
        }

        foreach (var i in _enemyBlocksFilter)
        {
            ref var block = ref _enemyBlocksFilter.Get1(i);
            block.Duration -= Time.deltaTime;

            if (block.Duration <= 0) _enemyBlocksFilter.GetEntity(i).Del<EnemyBehaviourBlock>();
        }
    }
}

