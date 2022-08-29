using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс для объектов, которым может наноситься урон
public interface IDamagable
{
    public void TakeDamage(string damageType);
}
