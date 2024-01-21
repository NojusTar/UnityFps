using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        healthSlider = GetComponent<Slider>();

        healthSlider.maxValue = player.GetPlayerMaxHp();
    }

    private void Update()
    {
        healthSlider.value = player.GetPlayerHp();
    }
}
