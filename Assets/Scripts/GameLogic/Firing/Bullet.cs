using Leopotam.Ecs;
using Voody.UniLeo;
using UnityEngine;

public class Bullet : DamagingObject
{
    protected override void TriggerEnterAction(Collider collision)
    {
        base.TriggerEnterAction(collision);

        Despawn();
    }

    public override void Despawn()
    {
        CreateDespawnEntity();
  
        base.Despawn();
    }

    protected virtual void CreateDespawnEntity()
    {
        _world = WorldHandler.GetWorld();
        _entity = _world.NewEntity();
        _entity.Get<TransformComponent>().MyTransfrom = transform;
        _entity.Get<BulletHitTag>();
    }

}
