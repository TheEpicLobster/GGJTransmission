using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour {

    List<GameObject> enemies;
    GameObject lockedEnemy;
    bool runPrune = true;

    private void Start()
    {
        enemies = new List<GameObject>();
        GameObject.Find("PlayerState").GetComponent<HealthManager>().playerDeath += GameOver;

    }

    private IEnumerable PruneDeadEnemies()
    {
        do
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
            yield return new WaitForSeconds(1.0f);
        } while (runPrune);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Virus")
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Virus")
        {
            if (other.gameObject == lockedEnemy)
            {
                lockedEnemy = null;
            }
            enemies.Remove(other.gameObject);
        }
    }

    public GameObject GetLockedEnemy()
    {
        // Return the enemy
        if (lockedEnemy != null)
        {
            return lockedEnemy;
        }

        // Get a new enemy to lock on to
        if (enemies.Count == 0)
        {
            return null;
        }

        int randIdx = Random.Range(0, enemies.Count);
        // Loop until surviving enemy is located
        do
        {
            lockedEnemy = enemies[randIdx];
            randIdx = (randIdx + 1) % enemies.Count;
        } while (lockedEnemy == null);
        return lockedEnemy;
    }

    private void GameOver()
    {
        runPrune = false;
        enemies.Clear();
    }
}
