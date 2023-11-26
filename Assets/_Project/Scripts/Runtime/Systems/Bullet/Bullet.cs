using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private BulletData bulletData;
    private Vector2 startPosition;
    private float currentDistance;
    private Rigidbody2D rb;
    private HealthSystem healthSystem;
    private bool isDidDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.SetHealth(bulletData.maxHealth);
        healthSystem.OnDie += StopBullet;
        Initialize();   
    }

    private void OnDisable()
    {
        healthSystem.OnDie -= StopBullet;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, startPosition);
        if(currentDistance > bulletData.maxDistance)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col is null) return;
        Debug.Log($"Collider {col.name}");

        if (bulletData.tagDamage.Contains(col.gameObject.tag))
        {
            if(col.TryGetComponent(out IDamageable damageable) && isDidDamage == false)
            {
                isDidDamage = true;
                damageable.TakeDamage(bulletData.damage);
            }
            DestroyBullet();
        }
    }

    public void Initialize()
    {
        startPosition = transform.position;
        rb.velocity = transform.up * bulletData.speed;
    }

    private void StopBullet()
    {
        rb.velocity = Vector2.zero;
    }

    private void DestroyBullet()
    {
        rb.velocity = Vector2.zero;
        healthSystem.TakeDamage(100);
    }
}
