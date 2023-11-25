using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyBaseController
{
    [SerializeField] private Transform shotSpawnPoint;
    private Shooter shooter;
    private bool isCanShot;

    protected override void Awake()
    {
        base.Awake();
        shooter = GetComponent<Shooter>();
    }

    protected override void Start()
    {
        base.Start();
        isCanShot = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void Attack()
    {
        if (isCanShot)
        {
            shooter.Shoot(enemyData.numberOfShots, enemyData.spreadAngle, enemyData.bulletPrefab, shotSpawnPoint);
            StartCoroutine(nameof(IDelayToShot));
        }
    }

    IEnumerator IDelayToShot()
    {
        isCanShot = false;
        yield return new WaitForSeconds(enemyData.delayBetweenShot);
        isCanShot = true;
    }

}
