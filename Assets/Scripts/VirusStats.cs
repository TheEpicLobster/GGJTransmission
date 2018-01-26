﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStats : MonoBehaviour {
    [HideInInspector]
    public HealthManager player;

    public int health;
    [Range(0, 20)]
    public int speed;
    [Range(0, 999)]
    public int damage;

    private int curHealth;

    void Start()
    {
        curHealth = health;
    }

    private void Update()
    {
        TakeDamage(1);
    }

    public void DamagePlayer()
    {
        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        curHealth = (curHealth <= 0) ? 0 : curHealth;
        GetComponentInChildren<UnityEngine.UI.Slider>().value = curHealth;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
