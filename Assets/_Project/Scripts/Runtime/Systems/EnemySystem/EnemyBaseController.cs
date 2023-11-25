using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody2D))]
public abstract class EnemyBaseController : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    private NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (collider.gameObject.CompareTag("Player"))
        {
            Attack();
            Debug.Log("PLAYER");
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

    protected abstract void Attack();

}
