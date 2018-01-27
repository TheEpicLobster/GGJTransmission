using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    BankAccount bank;

    public GameObject basic;
    public GameObject fast;
    public GameObject multi;
    public GameObject sniper;

    public GameObject currentPlacement;

	// Use this for initialization
	void Start () {
        bank = GetComponent<BankAccount>();
        currentPlacement = null;
    }


    private void Update()
    {
        Debug.Log(currentPlacement);
        if (currentPlacement != null)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(transform.position);

            if (Input.GetMouseButtonUp(0))
            {
                if (true)
                {
                    currentPlacement.SetActive(true);
                    
                }
                else
                {
                    bank.Refund(currentPlacement.GetComponent<TowerStats>().GetStats().price);
                    Destroy(currentPlacement);
                }
                currentPlacement = null;
            }
        }
    }

    public void PurchaseBasic()
    {
        if (bank.Purchase(basic.GetComponent<TowerStats>().GetStats().price))
        {
            currentPlacement = Instantiate(basic, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            currentPlacement.SetActive(false);

            Debug.Log(currentPlacement);
        }
    }

    public void PurchaseFast()
    {

    }

    public void PurchaseMulti()
    {

    }

    public void PurchaseSniper()
    {

    }
}

