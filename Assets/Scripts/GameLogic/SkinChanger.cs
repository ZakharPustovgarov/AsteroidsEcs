using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _polySprite;
    [SerializeField] private Sprite _spriteSprite;

    private EcsWorld _world = null;

    private void Start()
    {
        _world = WorldHandler.GetWorld();

        _world.NewEntity().Get<SkinChangerComponent>().SkinChanger = this;
    }

    public void ChangeGraphic(GraphicType type)
    {
        switch(type)
        {
            case GraphicType.POLY:
                if(_spriteRenderer.sprite != _polySprite) _spriteRenderer.sprite = _polySprite;
                break;
            case GraphicType.SPRITE:
                if (_spriteRenderer.sprite != _spriteSprite) _spriteRenderer.sprite = _spriteSprite;
                break;
        }
    }
}

