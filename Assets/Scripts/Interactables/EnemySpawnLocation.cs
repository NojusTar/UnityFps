using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{

    [SerializeField]
    public GameObject enemy;

    [Header("Base Enemy Settings")]
    [SerializeField]
    private float maxHp;

    [Header("Range Enemy Settings")]
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletUpVelocity;

    public void spawnEnemies()
    {
        enemy = Instantiate(enemy, transform.position, Quaternion.identity);

        if (enemy.GetComponent<BaseEnemy>())
        {
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
            if (maxHp != 0)
            {
                baseEnemy.maxHp = maxHp;
            }
        }

        if (enemy.GetComponent<RangeEnemy>() )
        {
            RangeEnemy rangeEnemy = enemy.GetComponent<RangeEnemy>();
            if (timeBetweenAttacks != 0)
            {
                rangeEnemy.timeBetweenAttacks = timeBetweenAttacks;
            }
            if (attackRange != 0)
            {
                rangeEnemy.attackRange = attackRange;
            }
            if (bulletSpeed != 0)
            {
                rangeEnemy.bulletSpeed = bulletSpeed;
            }
            if (bulletUpVelocity != 0)
            {
                rangeEnemy.bulletUpVelocity = bulletUpVelocity;
            }
        }
    }
}
