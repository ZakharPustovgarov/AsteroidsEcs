using Leopotam.Ecs;

sealed class DamageSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformComponent, HealthComponent>.Exclude<IsDeathTag> _entityHealthFilter = null;
    private readonly EcsFilter<TransformComponent, DamageComponent> _damageFilter = null;


    public void Run()
    {
        if (_entityHealthFilter.IsEmpty() || _damageFilter.IsEmpty()) return;

        foreach (var i in _damageFilter)
        {
            ref var trans1 = ref _damageFilter.Get1(i);
            ref var damage = ref _damageFilter.Get2(i);

            foreach (var j in _entityHealthFilter)
            {
                ref var trans2 = ref _entityHealthFilter.Get1(j);
                ref var health = ref _entityHealthFilter.Get2(j);

                if (trans1.MyTransfrom == trans2.MyTransfrom)
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
