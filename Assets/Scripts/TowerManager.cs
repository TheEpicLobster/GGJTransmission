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

    private bool OffTrack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        List<RaycastHit> res = new List<RaycastHit>(Physics.RaycastAll(ray, float.MaxValue));
        res.Sort( (lhs, rhs) => { return (int)(lhs.distance - rhs.distance); } );
        foreach (var h in res)
        {
            var col = h.collider;
            Renderer rend = h.transform.GetComponent<Renderer>();
            Texture2D tex = rend.material.mainTexture as Texture2D;
            var xInTex = (int)(h.textureCoord.x * tex.width);
            var yInTex = (int)(h.textureCoord.y * tex.height);
            var pix = tex.GetPixel(xInTex, yInTex);
            if (pix.a > 0)
            {
                return h.transform.gameObject.tag == "FieldOfPlay";
            }
        }
        return false;
    }

    private void Update()
    {
        if (currentPlacement != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPlacement.transform.position = new Vector3(pos.x, pos.y, currentPlacement.transform.position.z);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentPlacement.transform.GetChild(0).gameObject.GetComponent<IntersectionTracking>().CanPlace() && OffTrack())
                {
                    Destroy(currentPlacement.transform.GetChild(0).gameObject.GetComponent<IntersectionTracking>());
                    currentPlacement.GetComponent<ShootEnemies>().enabled = true;
                }
                else
                {
                    RefundPurchase();
                }
                currentPlacement = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPlacement != null)
            {
                RefundPurchase();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            PurchaseBasic();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            PurchaseFast();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            PurchaseMulti();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            PurchaseSniper();
        }

    }

    void PurchaseTower(GameObject prefab)
    {
        if (currentPlacement != null)
        {
            RefundPurchase();
        }

        if (bank.Purchase(prefab.GetComponent<TowerStats>().GetStats().price))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPlacement = Instantiate(prefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            currentPlacement.GetComponent<ShootEnemies>().enabled = false;
            currentPlacement.transform.GetChild(0).gameObject.AddComponent<IntersectionTracking>();
        }
    }

    public void PurchaseBasic()
    {
        PurchaseTower(basic);
    }

    public void PurchaseFast()
    {
        PurchaseTower(fast);
    }

    public void PurchaseMulti()
    {
        PurchaseTower(multi);
    }

    public void PurchaseSniper()
    {
        PurchaseTower(sniper);
    }

    public void RefundPurchase()
    {
        bank.Refund(currentPlacement.GetComponent<TowerStats>().GetStats().price);
        Destroy(currentPlacement);
        currentPlacement = null;
    }
}

