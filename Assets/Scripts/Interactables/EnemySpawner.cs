using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnLocation[] enemySpawnLocations;
    [SerializeField]
    private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        foreach (EnemySpawnLocation enemy in enemySpawnLocations)
        {
            enemy.spawnEnemies();
        }

        Destroy(gameObject);
    }
}
