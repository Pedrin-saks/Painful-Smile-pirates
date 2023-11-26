using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : EnemyBaseController
{
    private bool isDidDamage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack(Collider2D col)
    {
        if (col is null) return;
        if (col.TryGetComponent(out IDamageable damageable) && isDidDamage == false)
        {
            isDidDamage = true;
            damageable.TakeDamage(enemyData.damage);
            healthSystem.TakeDamage(100);
        }
    }
}
