using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    [Range(0, 999)]
    public int defaultHealth = 0;
    [HideInInspector]
    public int health;

    public System.Action playerDeath;
    public System.Action<int> healthUpdated;

    public void Awake()
    {
        health = defaultHealth;
        playerDeath += ResetHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0 || GameStateManager.gameState != GameStateManager.State.InProgress)
        {
            // Already dead
            return;
        }

        health -= damage;

        health = (health < 0) ? 0 : health;

        // Alert change in player health
        if (healthUpdated != null)
        {
            healthUpdated.Invoke(health);
        }

        // Alert game over
        if (health <= 0)
        {
            if (playerDeath != null)
            {
                playerDeath.Invoke();
            }
        }
    }

    public void ResetHealth()
    {
        health = defaultHealth;
    }
}
