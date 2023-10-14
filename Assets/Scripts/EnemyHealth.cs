using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : DamageableEntity
{
    protected override void OnDeath()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        base.OnDeath();
    }
}
