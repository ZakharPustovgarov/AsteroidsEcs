using Leopotam.Ecs;
using UnityEngine;

sealed class GunFireSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTag, GunComponent>.Exclude<FiringBlock> _firingGunsFilter = null;
    private readonly EcsFilter<GunFireEvent> _fireFilter = null;  

    private EcsWorld _world = null;
    private GameData _gameData = null;
    private ObjectFactoryWithPool<Bullet> _bulletFactory = null;
    private ObjectFactoryWithPool<Laser> _laserFactory = null;



    public void Run()
    {
        if (_firingGunsFilter.IsEmpty()) return;

        foreach(var i in _fireFilter)
        {
            ref var gun = ref _firingGunsFilter.Get2(0);
            ref var fireEvent = ref _fireFilter.Get1(i);
          
            if(fireEvent.FireType == FireType.BULLET)
            {
                CreateBullet(ref gun);
            }
            else if (fireEvent.FireType == FireType.LASER)
            {
                CreateLaser(ref gun);
            }

        }
    }

    private void CreateBullet(ref GunComponent gun)
    {
        Bullet bullet = _bulletFactory.Spawn();

        bullet.transform.position = gun.Barrel.position;

        var bulletEntity = _world.NewEntity();

        bullet.transform.up = gun.Muzzle.up;

        bulletEntity.Get<TransformComponent>().MyTransfrom = bullet.transform;
        bulletEntity.Get<BulletLifeTime>().Time = 2f;
        bulletEntity.Get<BulletComponent>().Speed = _gameData.BulletFlyingSpeed;
        bulletEntity.Get<BulletComponent>().Bullet = bullet;
        bulletEntity.Get<DirectionComponent>().Direction = Vector3.Normalize(gun.Muzzle.position - gun.Barrel.position);
    }

    private void CreateLaser(ref GunComponent gun)
    {
        Laser laser = _laserFactory.Spawn();

        laser.transform.position = gun.Barrel.position;

        var LaserEntity = _world.NewEntity();

        laser.transform.up = gun.Muzzle.up;

        LaserEntity.Get<LaserComponent>().Laser = laser;
        LaserEntity.Get<TransformComponent>().MyTransfrom = laser.transform;
        LaserEntity.Get<CounterComponent>().Duration= 4f;
    }
}
