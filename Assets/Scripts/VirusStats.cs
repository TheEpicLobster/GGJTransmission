using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStats : MonoBehaviour {
    [HideInInspector]
    public GameObject player;

    public int health;
    [Range(0, 20)]
    public int speed;
    [Range(0, 999)]
    public int damage;

    [Range(0, 1000)]
    public int payout = 100;

    private int curHealth;

    void Start()
    {
        curHealth = (int)(health * ChemistryCalls.VirusHealthMult);
    }

    public void DamagePlayer()
    {
        player.GetComponent<HealthManager>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        curHealth = (curHealth <= 0) ? 0 : curHealth;
        GetComponentInChildren<UnityEngine.UI.Slider>().value = curHealth;
        if (curHealth <= 0)
        {
            player.GetComponent<BankAccount>().ReceivePayout(payout);
            Destroy(gameObject);
        }
    }
}
