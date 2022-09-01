using Leopotam.Ecs;
using UnityEngine;

sealed class PlayerKeyboardInputSystem : IEcsRunSystem
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

            if (inputData.IsRotatingLeft) entity.Get<RotateComponent>().RotateType = RotateType.LEFT;

            if (inputData.IsRotatingRight) entity.Get<RotateComponent>().RotateType = RotateType.RIGHT;

            if (inputData.IsMovingForward)
            {
                entity.Get<DirectionComponent>().Direction = _playerFilter.Get2(i).MyTransfrom.up;
                entity.Get<MoveSpeedComponent>().Speed = _gameData.FlyingSpeed;
            }

            if (inputData.IsFiringBullets)
            {
                //Debug.Log("Setted fire event for bullet");
                entity.Get<GunFireEvent>().FireType = FireType.BULLET;
            }
            if (inputData.IsFiringLaser)
            {
                //Debug.Log("Setted fire event for laser");
                entity.Get<GunFireEvent>().FireType = FireType.LASER; 
            }
        }
    }

    private InputData CollectInput()
    {
        var inputData = new InputData();

        if (Input.GetKey(_gameData.FlyForwardKey)) inputData.IsMovingForward = true;
        
        if (Input.GetKey(_gameData.RotateLeftKey)) inputData.IsRotatingLeft = true;

        if (Input.GetKey(_gameData.RotateRightKey)) inputData.IsRotatingRight = true;

        if (Input.GetKey(_gameData.FireBulletKey))
        {
            inputData.IsFiringBullets = true;
        }

        if (Input.GetKeyDown(_gameData.FireLaserKey)) inputData.IsFiringLaser = true;

        return inputData;
    }
}

