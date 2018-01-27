using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour {

    List<GameObject> enemies;
    GameObject lockedEnemy;

    private void Start()
    {
        enemies = new List<GameObject>();
        GameObject.Find("PlayerState").GetComponent<HealthManager>().playerDeath += GameOver;

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
        if (enemies.Count == 0)
        {
            return null;
        }

        int nullCount = 0;
        enemies.Sort((lhs, rhs) => 
        {
            if (rhs == null)
            {
                nullCount++;
                return -1;
            }
            if (lhs == null)
            {
                nullCount++;
                return 1;
            }
            return (int)(rhs.GetComponent<FollowPath>().distanceTravelled - lhs.GetComponent<FollowPath>().distanceTravelled);
        });

        if (enemies[0] == null)
        {
            enemies.Clear();
            return null;
        }

        if (nullCount != 0)
        {
            enemies.RemoveRange(enemies.Count - nullCount, nullCount);
        }

        return enemies[0];
    }

    private void GameOver()
    {
        enemies.Clear();
    }

    private void Restart()
    {

    }
}
