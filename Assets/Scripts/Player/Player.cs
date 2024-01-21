using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHp;
    private float hp;

    private void Start()
    {
        hp = maxHp;

    }
    private void Update()
    {
        if (hp <= 0)
        {
            HandleDeath();
        }
        
    }

    private void HandleDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

    }

    public float GetPlayerHp()
    {
        return hp;
    }

    public float GetPlayerMaxHp()
    {
        return maxHp;
    }

    public void HealPlayer(float sum)
    {
        hp += sum;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

}
