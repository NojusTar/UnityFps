using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingEnemy : BaseEnemy
{

    [Header("settings")]
    //attacking
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float fieldOfImpact;


    [Header("variables")]
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

        if (!alreadyAttacked && agent.enabled != false)
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

        Debug.Log("explode");
        // TODO sutvarkyti kad castintu normaliai nes kazkodel nepaeina ir nesprogsta
        
        Explode();
        Debug.Log("nuva prisidarei");
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fieldOfImpact);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
                rb.AddExplosionForce(explosionForce, transform.position, fieldOfImpact);
                rb.drag = 0f;

            }
            if (collider.GetComponent<Player>())
            {
                collider.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
        Destroy(gameObject);
    }
}
