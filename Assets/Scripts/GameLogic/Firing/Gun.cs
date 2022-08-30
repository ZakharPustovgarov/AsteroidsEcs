using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _muzzle;

    private EcsEntity _entity;

    public void Construct(EcsEntity entity)
    {
        _entity = entity;

        ref var gun = ref _entity.Get<GunComponent>();

        gun.Barrel = _barrel;
        gun.Muzzle = _muzzle;
    }
}
