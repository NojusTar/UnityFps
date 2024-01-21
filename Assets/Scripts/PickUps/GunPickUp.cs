using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    [SerializeField]
    private Gun gun;
    

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GunInventory playerGuns = GameObject.Find("GunHolder").GetComponent<GunInventory>();
            playerGuns.PickUpGun(gun);
            Destroy(gameObject);
        }
        
        
    }

}
