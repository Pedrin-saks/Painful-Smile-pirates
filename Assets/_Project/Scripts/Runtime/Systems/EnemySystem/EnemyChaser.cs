using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : EnemyBaseController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        Debug.Log("Explosions");
        gameObject.SetActive(false);
    }
}
