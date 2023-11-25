using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float currentHealth { get; set; }
    [field: SerializeField] public float maxHealth { get; set; }

    [SerializeField] private EnemyData enemyData;
    private SpriteRenderer sr;
    private Animator anim;

    public event Action<float, float> OnChangeHealth;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }

        Parallel.ForEach(enemyData.healthSettings, healthSetting => 
        {
            if (currentHealth <= healthSetting.lifePercentage)
            {
                sr.sprite = healthSetting.healthSprite;
            }       
        });

    }

    public void Die()
    {
        anim.SetTrigger("Explosions");
    }

    public void AnimationCompleted()
    {
        Destroy(this.gameObject);
    }
}
