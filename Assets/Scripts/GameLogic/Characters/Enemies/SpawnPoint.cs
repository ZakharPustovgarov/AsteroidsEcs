using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        var entity = WorldHandler.GetWorld().NewEntity();
        entity.Get<SpawnPointTag>();
        entity.Get<TransformComponent>().MyTransfrom = transform;
    }
}
