using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

sealed class BulletStopSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformComponent, BulletComponent> _bulletsFilter = null;
    private readonly EcsFilter<TransformComponent, BulletHitTag> _deathTransformFilter = null;

    private ObjectFactoryWithPool<Bullet> _bulletFactory = null;

    public void Run()
    {
        if (_deathTransformFilter.IsEmpty()) return;
        //Debug.Log("<color=red>Death transforms count: </color>" + _deathTransformFilter.GetEntitiesCount());
        //Debug.Log("<color=red>Active bullets count: </color>" + _bulletsFilter.GetEntitiesCount());

        foreach (var j in _deathTransformFilter)
        {
            var deathTransform = _deathTransformFilter.Get1(j).MyTransfrom;

            foreach (var i in _bulletsFilter)
            {
                var bulletTransform = _bulletsFilter.Get1(i).MyTransfrom;

                if (deathTransform == bulletTransform)
                {
                    ref var entity = ref _bulletsFilter.GetEntity(i);
                    ref var entityDeath = ref _deathTransformFilter.GetEntity(j);

                     var bullet =  entity.Get<BulletComponent>().Bullet;
                    //Debug.Log("Death - " + bulletTransform.name + ", color - " + _bulletsFilter.Get2(i).Color);
                    entity.Destroy();
                    entityDeath.Destroy();

                    //_bulletFactory.Despawn(bullet);
                    break;
                }
            }
        }

        List<EcsEntity> entities = new List<EcsEntity>();

        foreach(var i in _deathTransformFilter)
        {
            entities.Add(_deathTransformFilter.GetEntity(i));
        }

        foreach (var entity in entities)
        {
            entity.Destroy();
        }


    }
}


