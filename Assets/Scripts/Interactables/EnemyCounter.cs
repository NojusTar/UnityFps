using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public int enemyCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Enemy entered the room
            enemyCounter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Enemy exited the room (destroyed)
            enemyCounter--;
        }
    }

    // You can call this function from other parts of your code to get the current enemy count
    public int GetEnemyCount()
    {
        return enemyCounter;
    }
}
