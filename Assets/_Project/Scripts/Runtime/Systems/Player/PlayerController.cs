using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform shotSpawnPoint;
    [SerializeField] private Transform tripleShotSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody2D rb;
    private Shooter shooter;
    [SerializeField] private Vector2 movementInput;

    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        GameManager.Instance.PlayerRegister(transform);
        InputManager.Instance.ButtonShootDown += OneShot;
        InputManager.Instance.ButtonTripleShootDown += TripleShot;
    }

    private void OnDisable()
    {
        InputManager.Instance.ButtonShootDown -= OneShot;
        InputManager.Instance.ButtonTripleShootDown -= TripleShot;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(InputManager.Instance.movement.x, InputManager.Instance.movement.y);
        CalculateSpeed(movementInput);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotationPlayer();
    }


    private void MovePlayer()
    {
        rb.velocity = transform.up * currentSpeed;
    }

    private void RotationPlayer()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * rotateSpeed));
    }

    private void CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.y > 0)
            currentSpeed += acceleration * Time.deltaTime;
        else
            currentSpeed -= deacceleration * Time.deltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);
    }

    private void OneShot()
    {
        shooter.Shoot(1, 0, bulletPrefab, shotSpawnPoint);
    }

    private void TripleShot()
    {
        shooter.Shoot(3, 45, bulletPrefab, tripleShotSpawnPoint);
    }

}
