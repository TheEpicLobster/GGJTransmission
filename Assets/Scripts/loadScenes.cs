using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScenes : MonoBehaviour {

    public void loadScene()
    {
        //Application.LoadLevel("TowerDefence");
        //loadScene("TowerDefence");
        //UnityEngine.SceneManagement.loa
        Debug.Log("Hi");
        SceneManager.LoadScene("Scenes/TowerDefence");

    }
}
