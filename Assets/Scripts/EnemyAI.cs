using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Transform target;
    NavMeshAgent navMeshAgent;
    EnemyHealth enemyHealth;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        // when dead disable these elements
        if (enemyHealth.IsDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }

        // measuring position between us and enemy
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked && navMeshAgent.enabled == true)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        // FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
    }

    // where to look at
    //TODO look at rotation not working right because AI working correctly and no need to set up it
    // void FaceTarget()
    // {
    //     // normalized return magnitude 1
    //     Vector3 direction = (target.position - target.position).normalized;
    //     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //     // slerp is spherical interpolation
    //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
