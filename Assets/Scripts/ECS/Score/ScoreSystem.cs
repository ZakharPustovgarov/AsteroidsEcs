using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

sealed class ScoreSystem : IEcsRunSystem
{
    private readonly EcsFilter<ScoreTextComponent> _textsFilter;
    private readonly EcsFilter<ScoreAddedComponent> _addingFilter;

    private int _score = 0;

    public void Run()
    {
        if (_addingFilter.IsEmpty() || _textsFilter.IsEmpty()) return;

        foreach(var i in _textsFilter)
        {
            _score += _addingFilter.Get1(i).Score;
        }

        foreach(var i in _textsFilter)
        {
            _textsFilter.Get1(i).Text.text = Convert.ToString(_score);
        }
    }
}
