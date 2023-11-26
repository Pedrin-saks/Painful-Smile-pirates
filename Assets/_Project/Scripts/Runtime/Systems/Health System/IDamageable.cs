using System;
using System.Collections.Generic;

public interface IDamageable
{
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }
    public event Action<float, float> OnChangeHealth;
    public event Action OnDie;
    public void SetHealth(float health, List<HealthSettings> healthSettings);
    public void TakeDamage(float damage);
    public void Die();
}
