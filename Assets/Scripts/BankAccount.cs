using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccount : MonoBehaviour {
    public int balance;

    public System.Action<int> incomeChanged;

    private void Awake()
    {
        GameObject spawner = GameObject.Find("Spawner");
        VirusSpawner spawnerScript = spawner.GetComponent<VirusSpawner>();
        spawnerScript.waveComplete += GetWaveBonus;
    }

    void GetWaveBonus( int waveId, VirusSpawner.Wave wave)
    {
        ReceivePayout(wave.bonus);
    }

    public void ReceivePayout(int size)
    {
        balance += size;
        CallIncomeChanged();
    }


    public bool Purchase(int cost)
    {
        if (cost > balance)
        {
           
            return false;
        }

        balance -= cost;
        CallIncomeChanged();
        return true;
    }

    public void Refund(int value)
    {
        balance += value;
        CallIncomeChanged();
    }

    void CallIncomeChanged()
    {
        if (incomeChanged != null)
        {
            incomeChanged.Invoke( balance );
        }
    }

}
