using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStats : MonoBehaviour {
    public HealthManager player;

    public int health = 10;
    [Range(0, 20)]
    public int speed = 1;
    [Range(0, 999)]
    public int damage = 1;

    public void DamagePlayer()
    {
        player.TakeDamage(damage);
    }
}
