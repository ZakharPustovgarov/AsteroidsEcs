using Leopotam.Ecs;

using System;
using System.Collections.Generic;
using UnityEngine;

sealed class FinishSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTag, IsDeathTag> _deadPlayerFilter = null;
    private readonly EcsFilter<DeathScreenComponent> _deathScreenFilter = null;

    private EcsWorld _world = null;

    private bool _isFinished = false;

    private GameData _gameData;


    public void Run()
    {
        if (_isFinished || _deadPlayerFilter.IsEmpty()) return;
        
        _isFinished = true;

        _deathScreenFilter.Get1(0).DeathScreen.Show();
    }
}


