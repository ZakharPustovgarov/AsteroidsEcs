using Leopotam.Ecs;
using UnityEngine;

sealed class PlayerInputSystem : IEcsRunSystem
{

    private readonly EcsFilter<PlayerTag, TransformComponent>.Exclude<IsDeathTag> _playerFilter = null;
    private readonly EcsFilter<PermaBlock> _permaBlockFilter = null;

    private GameData _gameData = null;



    public void Run()
    {
        //Debug.Log("Players in scene: " + _playerFilter.GetEntitiesCount());
        if (!_permaBlockFilter.IsEmpty()|| _playerFilter.IsEmpty()) return;

        InputData inputData = CollectInput();

        foreach (var i in _playerFilter)
        {
            ref var entity = ref _playerFilter.GetEntity(i);
 
        }
    }

    private InputData CollectInput()
    {
        var inputData = new InputData();

        if (Input.GetKeyDown(_gameData.FlyForwardKey)) inputData.IsMovingForward = true;
        
        if (Input.GetKeyDown(_gameData.RotateLeftKey)) inputData.IsRotatingLeft = true;

        if (Input.GetKeyDown(_gameData.RotateRightKey)) inputData.IsRotatingRight = true;

        if (Input.GetKeyDown(_gameData.FireBulletKey)) inputData.IsFiringBullets = true;

        if (Input.GetKeyDown(_gameData.FireLaserKey)) inputData.IsFiringLaser = true;

        return inputData;
    }
}

