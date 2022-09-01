using Leopotam.Ecs;

sealed class LaserStopSystem : IEcsRunSystem
{
    private readonly EcsFilter<LaserComponent, CountEndEvent> _laserEndEvent = null;

    private ObjectFactoryWithPool<Laser> _laserFactory = null;

    public void Run()
    {
        if (_laserEndEvent.IsEmpty()) return;

        foreach(var i in _laserEndEvent)
        {
            Laser laser = _laserEndEvent.Get1(i).Laser;
            if (laser == null) continue;
            _laserFactory.Despawn(_laserEndEvent.Get1(i).Laser);
        }
    }
}


