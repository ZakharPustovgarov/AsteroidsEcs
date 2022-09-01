using Leopotam.Ecs;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using System.Collections.Generic;
using Voody.UniLeo;

public class DamagingObject : MonoBehaviour
{
    public int Damage;

    protected EcsWorld _world;
    protected EcsEntity _entity;

    CompositeDisposable disposables = new CompositeDisposable();

    private void Start()
    {
        _world = WorldHandler.GetWorld();
    }

    protected virtual void TriggerEnterAction(Collider2D collision)
    {
        //Debug.Log("<color=orange>Bullet hitted </color>" + collision.name, collision);
        var entity =  _world.NewEntity();
        entity.Get<TransformComponent>().MyTransfrom = collision.transform;
        ref var damage = ref entity.Get<DamageComponent>();

        damage.Damage = Damage;
        damage.Damaged = collision.transform;

        //Debug.Log("Damaged " + collision.name, collision.transform);
    }

    public virtual void Spawn()
    {
        this.OnTriggerEnter2DAsObservable().Subscribe(o => TriggerEnterAction(o)).AddTo(disposables);
        //Debug.Log("Damage object spawned", this.transform);
    }


    public virtual void Despawn()
    {
        disposables.Clear();
    }

    protected virtual void OnDestroy()
    {
        disposables.Dispose();
    }
}
