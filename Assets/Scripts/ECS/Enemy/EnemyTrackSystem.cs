using System;
using Leopotam.Ecs;
using UnityEngine;

sealed class EnemyTrackSystem : IEcsRunSystem
{
    private readonly EcsFilter<UfoTag, TransformComponent> _ufoFilter = null;
    private readonly EcsFilter<PlayerTag, TransformComponent> _playerFilter = null;

    private GameData _gameData = null;

    public void Run()
    {
        if (_ufoFilter.IsEmpty() || _playerFilter.IsEmpty()) return;

        ref var playerTrans = ref _playerFilter.Get2(0).MyTransfrom;

        foreach (var i in _ufoFilter)
        {
            ref var entity = ref _ufoFilter.GetEntity(i);
            ref var ufoTrans = ref _ufoFilter.Get2(i).MyTransfrom;

            entity.Get<DirectionComponent>().Direction = Vector3.Normalize(playerTrans.position - ufoTrans.position);
            entity.Get<MoveSpeedComponent>().Speed = _gameData.UfoFlyingSpeed;
        }
    }
}
