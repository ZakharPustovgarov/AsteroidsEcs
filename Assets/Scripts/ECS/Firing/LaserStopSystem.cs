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
            _laserFactory.Despawn(_laserEndEvent.Get1(i).Laser);
        }
    }
}


