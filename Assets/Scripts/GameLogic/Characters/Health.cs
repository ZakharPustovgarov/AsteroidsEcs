using Leopotam.Ecs;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public void Construct(EcsEntity entity)
    {
        ref var hp = ref entity.Get<HealthComponent>();
        hp.CurrentHealth = _maxHealth;
        hp.MaxHealth = _maxHealth;
    }
}
