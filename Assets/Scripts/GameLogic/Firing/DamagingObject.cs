using Leopotam.Ecs;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using System.Collections.Generic;

public class DamagingObject : MonoBehaviour
{
    public int Damage;

    protected EcsWorld _world;
    protected EcsEntity _entity;

    CompositeDisposable disposables = new CompositeDisposable();

    protected virtual void TriggerEnterAction(Collider collision)
    {
        //Debug.Log("<color=orange>Bullet hitted </color>" + collision.name, collision);
        var entity =  _world.NewEntity();
        ref var damage = ref entity.Get<DamageComponent>();

        damage.Damage = Damage;
        damage.Damaged = collision.transform;
    }

    public virtual void Spawn()
    {
        this.OnTriggerEnterAsObservable().Subscribe(o => TriggerEnterAction(o)).AddTo(disposables);
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
