using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : DamageableEntity
{
    [SerializeField] private int currencyValue = 10;
    protected override void OnDeath()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        LevelManager.Instance.IncreaseCurrency(currencyValue);
        base.OnDeath();
    }
}
