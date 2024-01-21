using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    public float maxHp;
    protected float hp;
    [SerializeField]
    protected float playerHeight;

    [SerializeField]
    private float timeToGetUp;

    [HideInInspector]
    public float getUpTimer = 0;

    protected bool isGrounded;

    [SerializeField]
    private EnemySpawner spawner;

    protected void Start()
    {
        hp = maxHp;

    }
    protected void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        HandleDeath();

        ResetOnGround();
    }

    protected virtual void HandleDeath()
    {
        if (hp <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;

    }

    private void ResetOnGround()
    {
        if (GetComponent<NavMeshAgent>() == null) { return; }

        if (isGrounded && GetComponent<NavMeshAgent>().enabled == false)
        {
            getUpTimer += Time.deltaTime;

        }

        if (getUpTimer >= timeToGetUp)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            Destroy(GetComponent<Rigidbody>());
            getUpTimer = 0;
        }
    }


}
