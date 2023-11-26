using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody2D), typeof(HealthSystem))]
public abstract class EnemyBaseController : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    private NavMeshAgent agent;
    protected HealthSystem healthSystem;
    private bool isDead;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        healthSystem.SetHealth(enemyData.maxHealth, enemyData.healthSettings);
        healthSystem.OnDie += SetIsDead;
    }

    private void OnDisable()
    {
        healthSystem.OnDie -= SetIsDead;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            agent.speed = 0;
        }
        agent.SetDestination(GameManager.Instance.PlayerPosition.position);
        UpdateRotation();
        CheckDetectionArea();
    }

    private void UpdateRotation()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            Vector3 direction = agent.steeringTarget - transform.position;
            direction.z = 0f;

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90f;

                Quaternion toRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, enemyData.rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void CheckDetectionArea()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, enemyData.radius, enemyData.layerMask);

        if (collider is null) return;
        if (collider.gameObject.CompareTag("PlayerBoat"))
        {
            Debug.Log($"Nome e tag: {collider.gameObject.name} / {collider.gameObject.tag}");
            Attack(collider);
            
        }
    }

    private void OnDrawGizmos()
    {
        if(enemyData is not null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyData.radius);
        }
    }

    private void SetIsDead()
    {
        isDead = true;
    }

    protected abstract void Attack(Collider2D collider);

}
