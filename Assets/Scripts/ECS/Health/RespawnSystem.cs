using Leopotam.Ecs;
using UnityEngine;

sealed class RespawnSystem : IEcsRunSystem
{
    private readonly EcsFilter<DeathComponent, IsDeathTag, RessurectCountEndEvent, HealthComponent> _deadFilter = null;
   // private readonly EcsFilter<GameFinishedEvent> _endGameEvent = null;

    private bool _blocked = false;

    public void Run()
    {
        if (_blocked) return;

        //if (!_endGameEvent.IsEmpty())
        //{
        //    _blocked = true;
        //    return;
        //}

        if (_deadFilter.IsEmpty() ) return;

        foreach(var i in _deadFilter)
        {
            ref var death = ref _deadFilter.Get1(i).DeathBringer;
            ref var health = ref _deadFilter.Get4(i);
            //Debug.Log("<color=blue>Reanimating</color>" + death.transform.name);
            health.CurrentHealth = health.MaxHealth;
            _deadFilter.GetEntity(i).Del<IsDeathTag>();
            death.Reanimate();
        }
    }
}

