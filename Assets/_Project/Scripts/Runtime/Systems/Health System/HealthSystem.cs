using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class HealthSystem : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float currentHealth { get; set; }
    [field: SerializeField] public float maxHealth { get; set; }

    public event Action<float, float> OnChangeHealth;
    public event Action OnDie;

    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private List<HealthSettings> healthSettings = new();

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void SetHealth(float health, List<HealthSettings> healthSettings = null)
    {
        currentHealth = maxHealth = health;

        if (healthSettings is null) return;

        foreach (var healthSet in healthSettings)
        {
            this.healthSettings.Add(healthSet);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthSettings is not null)
        {
            foreach (var healthSetting in healthSettings)
            {
                if (currentHealth <= healthSetting.lifePercentage)
                {
                    sr.sprite = healthSetting.healthSprite;
                }
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
        anim.enabled = true;
        if (CompareTag("EnemyBoat")) 
            EventsManager.OnEnemyDieTrigger();

        anim.SetTrigger("Explosions");
    }

    public void AnimationCompleted()
    {
        Destroy(this.gameObject);
    }
}
