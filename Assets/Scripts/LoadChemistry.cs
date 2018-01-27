﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChemistry : MonoBehaviour {

    public int waves = 5;

	// Use this for initialization
	void Awake () {
        GameObject spawner = GameObject.Find("Spawner");
        VirusSpawner spawnerScript = spawner.GetComponent<VirusSpawner>();
        spawnerScript.waveComplete += LoadChemistryScene;
    }

    // Update is called once per frame
    void LoadChemistryScene( int waveId, VirusSpawner.Wave wave)
    {
        if (waveId % waves == 0 && waveId / (waves * 2 + 1) == 0)
        {
            SceneManager.LoadScene("Scenes/Chemistry", LoadSceneMode.Additive);
        }
        else if (waveId % waves == 0 && waveId / (waves * 2 + 1) == 1)
        {
            SceneManager.LoadScene("Scenes/Chem2", LoadSceneMode.Additive);
        }
        //GameObject.Find("TowerDefenceObject").SetActive(false);
    }
}