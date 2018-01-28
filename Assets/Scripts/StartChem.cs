using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChem : MonoBehaviour {

    public GameObject canvas;


    public void StartGame()
    {
        canvas.SetActive(true);
        Destroy(gameObject);
    }
    public void Star()
    {
        canvas.SetActive(true);
    }
    public void close()
    {
        canvas.SetActive(false);
    }

    public void end()
    {
        Application.Quit();
    }
}
