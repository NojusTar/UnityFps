using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("paemei dash");
            other.gameObject.GetComponent<PlayerMovement>().hasDash = true;

            Destroy(gameObject);
        }
        
    }
}
