using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthNumbers : MonoBehaviour
{
    private TMP_Text healthNumbers;
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        healthNumbers = GetComponent<TMP_Text>();

        healthNumbers.text = player.GetPlayerHp().ToString("0");
    }

    private void Update()
    {
        healthNumbers.text = player.GetPlayerHp().ToString("0");
    }
}
