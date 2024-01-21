using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
        if (other.gameObject.GetComponent<MeshCollider>())
        {
            Destroy(gameObject);
        }
        
    }
}
