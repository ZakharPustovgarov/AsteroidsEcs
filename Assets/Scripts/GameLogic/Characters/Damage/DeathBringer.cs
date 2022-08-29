using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class DeathBringer : MonoBehaviour
{
    public Action DiedEvent;
    public Action ReanimatedEvent;

    private Vector3 _spawnPoint;

    private bool _isAlive;

    [SerializeField] private float _dieTime = 1.5f;

    private void Start()
    {
        _isAlive = true;
        _spawnPoint = transform.position;
    }

    public virtual void Die(bool withDestroy)
    {
        if (!_isAlive) return;
        _isAlive = false;
        DiedEvent?.Invoke();
        if (withDestroy) Observable.FromCoroutine(_ => Dying()).Subscribe().AddTo(this);
    }

    public virtual void Reanimate()
    {
        if (_isAlive) return;
        _isAlive = true;
        transform.position = _spawnPoint;
        ReanimatedEvent?.Invoke();
    }

    IEnumerator Dying()
    {
        if(_dieTime > 0) yield return new WaitForSeconds(_dieTime);

        Destroy(gameObject);
    }
}

public class DamageTaker : MonoBehaviour
{

}

