using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCountUpdater : MonoBehaviour {

    UnityEngine.UI.Text text;

	// Use this for initialization
	void Awake() {
        GameObject spawner = GameObject.Find("Spawner");
        VirusSpawner spawnerScript = spawner.GetComponent<VirusSpawner>();
        spawnerScript.newWave += NewWave;
        text = GetComponent<UnityEngine.UI.Text>();

    }

    void NewWave( int waveId, VirusSpawner.Wave waveStats)
    {
        text.text = "Wave: " + waveId.ToString();
    }
}
