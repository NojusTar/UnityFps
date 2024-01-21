using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedCounter : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private TMP_Text speedText;

    private Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + rb.velocity.magnitude.ToString("0.00");
    }
}
