using UnityEngine;
using Leopotam.Ecs;

sealed class BulletFlySystem : IEcsRunSystem
{
    private readonly EcsFilter<BulletComponent, ConstantDirection, TransformComponent, BulletLifeTime> _bulletFilter = null;

    private EcsWorld _world = null;
    public void Run()
    {
        if (_bulletFilter.IsEmpty()) return;

        //Debug.Log("Flying bullets count: " + _bulletFilter.GetEntitiesCount());
        foreach (var i in _bulletFilter)
        {
            ref var bullet = ref _bulletFilter.Get1(i);
            ref var direction = ref _bulletFilter.Get2(i);
            ref var transform = ref _bulletFilter.Get3(i);
            ref var lifeTime = ref _bulletFilter.Get4(i);
            lifeTime.Time -= Time.deltaTime;

            if (lifeTime.Time <= 0f)
            {
                var entity = _world.NewEntity();
                entity.Get<BulletHitTag>();
                entity.Get<TransformComponent>().MyTransfrom = transform.MyTransfrom;
                
                continue;
            }

            transform.MyTransfrom.position += direction.Direction * bullet.Speed * Time.deltaTime;

        }
    }
}

