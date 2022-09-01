using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leopotam.Ecs;
using Voody.UniLeo;

public class LaserCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private EcsWorld _world = null;

    private void Start()
    {
        _world = WorldHandler.GetWorld();
        var entity = _world.NewEntity().Get<LaserCounterComponent>().Text = _text;
    }

}
