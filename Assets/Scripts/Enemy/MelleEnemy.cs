using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using static UnityEngine.GraphicsBuffer;

public class MelleEnemy : BaseEnemy
{

    [Header("settings")]
    //attacking
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackDamage;


    [Header("variables")]
    [SerializeField]
    public NavMeshAgent agent;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask playerLayerMask;

    //states
    private bool alreadyAttacked;
    private bool playerInRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();

        if (hp <= 0) { return; }

        

        

        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);

        if (!playerInRange) { ChasePlayer(); }
        if (playerInRange) { AttackPlayer(); }
    }

    private void ChasePlayer()
    {
        if (gameObject != null && agent.enabled != false)
        {
            agent.SetDestination(player.position);
        }

    }

    private void AttackPlayer()
    {
        if (gameObject != null && agent.enabled != false)
        {
            agent.SetDestination(transform.position);
        }


        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        if (!alreadyAttacked && isGrounded && agent.enabled != false)
        {
            //attack code
            Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Attack()
    {


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit melleHit, attackRange))
        {
            if(melleHit.collider.gameObject.GetComponent<Player>())
            {
                melleHit.collider.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
            }

        }
    }

    
}
