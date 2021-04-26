using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    Animator animator;
    bool isDead = false;
    [SerializeField] float chaseDistance = 2f;
    [SerializeField] float turnSpeed = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //distance between the enemy and player
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > chaseDistance != isDead)
        {
           ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        animator.SetTrigger("isDead");
    }

    void ChasePlayer()
    {
        agent.updatePosition = true;
         agent.updatePosition = true;
         agent.SetDestination(target.position);

         animator.SetBool("isWalking", true);
         animator.SetBool("isAttacking",false);
    }

    void AttackPlayer()
    {
        agent.updateRotation = false;
        //Look at the player when attack
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        agent.updatePosition = false;

        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking",true);
    }
}
