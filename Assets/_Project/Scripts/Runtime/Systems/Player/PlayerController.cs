using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform shotSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(InputManager.Instance.movement.x, InputManager.Instance.movement.y);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotationPlayer();
    }


    private void MovePlayer()
    {
        rb.velocity = transform.up * movementInput.y * moveSpeed;
    }

    private void RotationPlayer()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * rotateSpeed));
    }
}
