using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScenes : MonoBehaviour {

    public void loadScene()
    {
        SceneManager.LoadScene("Scenes/TowerDefence");

    }

    public void loadInstructions()
    {

        SceneManager.LoadScene("Scenes/Instructions");
    }

    public void loadMenu()
    {

        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
