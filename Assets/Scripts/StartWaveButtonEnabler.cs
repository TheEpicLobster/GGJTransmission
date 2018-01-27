using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveButtonEnabler : MonoBehaviour {

    UnityEngine.UI.Button button;

	// Use this for initialization
	void Awake () {
        GameObject spawner = GameObject.Find("Spawner");
        VirusSpawner spawnerScript = spawner.GetComponent<VirusSpawner>();
        spawnerScript.newWave += WaveStarted;
        spawnerScript.waveComplete += WaveCompleted;
        button = GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void WaveStarted(int waveId, VirusSpawner.Wave wave)
    {
        button.interactable = false;
    }

    void WaveCompleted(int waveId, VirusSpawner.Wave wave)
    {
        button.interactable = true;
    }
}
