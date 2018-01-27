using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionTracking : MonoBehaviour {

    int towerIntersetcions = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "TowerFootprint")
        {
            towerIntersetcions++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "TowerFootprint")
        {
            towerIntersetcions--;
        }
    }

    public bool CanPlace()
    {
        Debug.Log(towerIntersetcions);
        return towerIntersetcions == 0;
    }
}
