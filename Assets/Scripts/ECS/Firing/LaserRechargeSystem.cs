using Leopotam.Ecs;
using System;

sealed class LaserRechargeSystem : IEcsRunSystem
{
    private readonly EcsFilter<LaserCountComponent> _fireFilter = null;
    private readonly EcsFilter<LaserCounterComponent> _textFilter = null;

    private GameData _gameData = null;

    public void Run()
    {
        if (_fireFilter.IsEmpty()) return;

        foreach (var i in _fireFilter)
        {
            ref var entity = ref _fireFilter.GetEntity(i);
            ref var laser = ref _fireFilter.Get1(i);

            if (entity.Has<LaserRechargeCountEnd>())
            {
                laser.Count++;

                if(laser.Count < laser.MaxCount)
                {
                    entity.Get<LaserRechargeCounterComponent>().Duration = _gameData.LaserRechargeTime;
                }

                //text.Text.text = Convert.ToString(laser.Count);
            }

            if(entity.Has<GunFireEvent>())
            {
                if(entity.Get<GunFireEvent>().FireType == FireType.LASER && !entity.Has<FiringBlock>())
                {
                    if(laser.Count <= 0) entity.Del<GunFireEvent>();
                    else
                    {
                        laser.Count--;
                        if(!entity.Has<LaserRechargeCounterComponent>()) entity.Get<LaserRechargeCounterComponent>().Duration = _gameData.LaserRechargeTime;

                        //text.Text.text = Convert.ToString(laser.Count);
                    }
                }
            }
        }


        foreach (var i in _textFilter)
        {
            ref var text = ref _textFilter.Get1(i);
            text.Text.text = Convert.ToString(_fireFilter.Get1(0).Count);
        }
    }
}
