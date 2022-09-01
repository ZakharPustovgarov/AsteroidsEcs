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
        if (_firingGunsFilter.IsEmpty() || _fireFilter.IsEmpty()) return;

        foreach(var i in _fireFilter)
        {
            ref var gun = ref _firingGunsFilter.Get2(0);
            ref var fireEvent = ref _fireFilter.Get1(i);
            ref var entity = ref _firingGunsFilter.GetEntity(i);


            if (fireEvent.FireType == FireType.BULLET)
            {
                //Debug.Log("Firing bullet");
                CreateBullet(ref gun);
                entity.Get<FiringBlock>().Duration = _gameData.BulletCooldown;
            }
            else if (fireEvent.FireType == FireType.LASER)
            {
                //Debug.Log("Firing laser");
                CreateLaser(ref gun);
                entity.Get<FiringBlock>().Duration = _gameData.LaserCooldown;
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
        bulletEntity.Get<BulletLifeTime>().Time = 5f;
        bulletEntity.Get<BulletComponent>().Speed = _gameData.BulletFlyingSpeed;
        bulletEntity.Get<BulletComponent>().Bullet = bullet;
        bulletEntity.Get<ConstantDirection>().Direction = Vector3.Normalize(gun.Muzzle.position - gun.Barrel.position);

        bullet.Spawn();
    }

    private void CreateLaser(ref GunComponent gun)
    {
        Laser laser = _laserFactory.Spawn();

        laser.transform.position = gun.Barrel.position;

        var LaserEntity = _world.NewEntity();

        laser.transform.up = gun.Muzzle.up;

        laser.transform.parent = gun.Barrel;


        LaserEntity.Get<LaserComponent>().Laser = laser;
        LaserEntity.Get<TransformComponent>().MyTransfrom = laser.transform;
        LaserEntity.Get<CounterComponent>().Duration = _gameData.LaserCooldown;

        laser.Spawn();
    }
}
