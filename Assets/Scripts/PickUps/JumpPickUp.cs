using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject visual;
    [SerializeField]
    private float resetTime;

    private PlayerMovement playerMovement;
    private bool pickedUp = false;
    private float resetCounter = 0f;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (pickedUp)
        {
            resetCounter += Time.deltaTime;
        }
        if (resetCounter >= resetTime)
        {
            pickedUp = false;
            resetCounter = 0f;
            visual.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (visual.activeSelf == true)
        {
            playerMovement.addAirJump();
            visual.SetActive(false);
            pickedUp = true;
        }
        

    }

    
}
