using UnityEngine;
using TMPro;
using Leopotam.Ecs;
using Voody.UniLeo;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private EcsWorld _world = null;

    private void Start()
    {
        _world = WorldHandler.GetWorld();

        _world.NewEntity().Get<ScoreTextComponent>().Text = _text;
    }
}
