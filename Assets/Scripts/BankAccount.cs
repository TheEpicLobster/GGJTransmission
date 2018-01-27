using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccount : MonoBehaviour {
    public float balance;
    public float incomePerSecond;

    private void Update()
    {
        balance += incomePerSecond * Time.deltaTime;
    }

    public bool Purchase(int cost)
    {
        if (cost > balance)
        {
            return false;
        }

        balance -= cost;
        return true;
    }

    public void Refund(int value)
    {
        balance += value;
    }

}
