using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter), typeof(Rigidbody2D), typeof(HealthSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Transform shotSpawnPoint;
    [SerializeField] private Transform tripleShotSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody2D rb;
    private Shooter shooter;
    private HealthSystem healthSystem;
    private Vector2 movementInput;
    private float currentSpeed;
    [SerializeField] private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        healthSystem = GetComponent<HealthSystem>();
        
    }

    private void Start()
    {
        healthSystem.SetHealth(playerData.maxHealth, playerData.healthSettings);
        GameManager.Instance.PlayerRegister(transform);
        InputManager.Instance.ButtonShootDown += OneShot;
        InputManager.Instance.ButtonTripleShootDown += TripleShot;
    }

    private void OnEnable()
    {
        healthSystem.OnDie += PlayerIsDead;
    }

    private void OnDisable()
    {
        InputManager.Instance.ButtonShootDown -= OneShot;
        InputManager.Instance.ButtonTripleShootDown -= TripleShot;
        healthSystem.OnDie -= PlayerIsDead;
    }

    void Update()
    {
        if (isDead == true) return;

        movementInput = new Vector2(InputManager.Instance.movement.x, InputManager.Instance.movement.y);
        CalculateSpeed(movementInput);
    }

    private void FixedUpdate()
    {
        if (isDead == true) return;

        MovePlayer();
        RotationPlayer();
    }


    private void MovePlayer()
    {
        rb.velocity = transform.up * currentSpeed;
    }

    private void RotationPlayer()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * playerData.rotateSpeed));
    }

    private void CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.y > 0)
            currentSpeed += playerData.acceleration * Time.deltaTime;
        else
            currentSpeed -= playerData.deacceleration * Time.deltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerData.moveSpeed);
    }

    private void OneShot()
    {
        shooter.Shoot(1, 0, bulletPrefab, shotSpawnPoint);
    }

    private void TripleShot()
    {
        shooter.Shoot(3, 45, bulletPrefab, tripleShotSpawnPoint);
    }

    private void PlayerIsDead()
    {
        isDead = true;
        EventsManager.OnFinishGameTrigger();
    }
}
