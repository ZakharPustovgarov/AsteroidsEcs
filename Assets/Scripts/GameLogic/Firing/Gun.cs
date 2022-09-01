using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _muzzle;

    [SerializeField] private int MaxLasers = 4;

    private EcsEntity _entity;

    public void Construct(EcsEntity entity)
    {
        _entity = entity;

        ref var gun = ref _entity.Get<GunComponent>();

        gun.Barrel = _barrel;
        gun.Muzzle = _muzzle;

        ref var laser = ref _entity.Get<LaserCountComponent>();
        laser.MaxCount = MaxLasers;
        laser.Count = MaxLasers;
    }
}
