using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : BaseEnemy
{

    [Header("settings")]
    //attacking
    [SerializeField]
    public float timeBetweenAttacks;
    [SerializeField]
    public float attackRange;
    [SerializeField]
    public float bulletSpeed;
    [SerializeField]
    public float bulletUpVelocity;


    [Header("variables")]
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform gunPoint;
    [SerializeField]
    private NavMeshAgent agent;
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
        Vector3 playerPos = player.position - transform.position;
        GameObject currentProjectile = Instantiate(projectile, gunPoint.transform.position, Quaternion.identity);
        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.AddForce(playerPos * bulletSpeed, ForceMode.Impulse);
        rb.AddForce(transform.up * bulletUpVelocity, ForceMode.Impulse);

        Destroy(currentProjectile, 5f);
    }
}
