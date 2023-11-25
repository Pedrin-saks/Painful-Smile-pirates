using System;
using UnityEngine;

public interface IDamageable
{
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }
    public event Action<float, float> OnChangeHealth;
    public void TakeDamage(float damage);
    public void Die();
}
