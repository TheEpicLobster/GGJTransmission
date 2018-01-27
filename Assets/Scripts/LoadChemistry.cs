using System.Collections;
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
        ChemistryCalls.towerDefence = GameObject.Find("TowerDefenceObject");
        if (waveId % waves == 0 && waveId / (waves * 2 + 1) == 0)
        {
            ChemistryCalls.timesCalled++;
            GetComponent<TowerManager>().RefundPurchase();
            ChemistryCalls.towerDefence.SetActive(false);
            SceneManager.LoadScene("Scenes/Chemistry", LoadSceneMode.Additive);
        }
        else if (waveId % waves == 0 && waveId / (waves * 2 + 1) == 1)
        {
            if (ChemistryCalls.timesCalled > 1)
            {
                ChemistryCalls.timesCalled = 0;
            }
            ChemistryCalls.timesCalled++;
            GetComponent<TowerManager>().RefundPurchase();
            ChemistryCalls.towerDefence.SetActive(false);
            SceneManager.LoadScene("Scenes/Chem2", LoadSceneMode.Additive);
        }
    }
}
