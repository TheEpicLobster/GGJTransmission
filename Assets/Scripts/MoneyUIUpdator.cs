using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUIUpdator : MonoBehaviour {

    public BankAccount manager;

    UnityEngine.UI.Text text;

    // Use this for initialization
    void Awake()
    {
        manager.incomeChanged += GainMonies;
        text = GetComponent<UnityEngine.UI.Text>();
    }

    private void Start()
    {
        text.text = manager.balance.ToString();
    }

    void GainMonies(int newBalance)
    {
        text.text = newBalance.ToString();
    }
}
