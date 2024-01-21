using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int sceneIndex;
    private GunInventory playerGuns;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerGuns = GameObject.Find("GunHolder").GetComponent<GunInventory>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            //save guns array to level var transfer
            if (LevelVarTransfer.Instance != null && playerGuns != null)
            {
                LevelVarTransfer.Instance.staticGunsArray.Clear();
                LevelVarTransfer.Instance.staticGunsArray.AddRange(playerGuns.GetComponent<GunInventory>().gunArray);
            }
            //save dash ability to level var transfer
            if (LevelVarTransfer.Instance != null && playerMovement != null)
            {
                LevelVarTransfer.Instance.hasDash = playerMovement.hasDash;
            }

                Debug.Log("player guns if not null");
        }

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
