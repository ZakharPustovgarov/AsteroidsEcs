using Leopotam.Ecs;
using UnityEngine;

sealed class DeathSystem : IEcsRunSystem
{
    private readonly EcsFilter<DeathComponent, IsDeathTag>.Exclude<RessurectCounterComponent, RessurectCountEndEvent> _deadFilter = null;

    public void Run()
    {
        if (_deadFilter.IsEmpty()) return;

        foreach(var i in _deadFilter)
        {
            ref var entity = ref _deadFilter.GetEntity(i);

            
            if (entity.Has<PlayerTag>())
            {
                _deadFilter.Get1(i).DeathBringer.Die(true);
                //entity.Get<RessurectCounterComponent>().Duration = 2f;
            }
            else if (entity.Has<MainEnemyTag>())
            {
                _deadFilter.Get1(i).DeathBringer.Die(false);
                entity.Get<RessurectCounterComponent>().Duration = 6f;
                //Debug.Log("<color=red>Enemy died in system: </color>" + _deadFilter.Get1(i).DeathBringer.name);
            }
            else
            {
                _deadFilter.Get1(i).DeathBringer.Die(true);
                //_deadFilter.GetEntity(i).Destroy();
            }                    
        }
    }
}

