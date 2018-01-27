using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour {
    public List<GameObject> virusPrefabs;

    public GameObject healthBarPrefab;

    [System.Serializable]
    public class Wave
    {
        public List<int> typeCount;
        [Range(0.1f, 10)]
        public float spawnInterval;
    }
    public List<Wave> waves;

    int waveId = -1;
    Wave wave;
    List<int> countRemaining;

    int countSinceLast;
    int spawnGap;

    List<GameObject> viruses;

    GameObject player;

    bool gameOver = false;

    bool waveRunning = false;

    // Use this for initialization
    void Start () {
        NewWave();
	}
	
	// Update is called once per frame
	void Update () {


        if (gameOver)
        {
            return;
        }

        if (!waveRunning)
        {
            if (!CheckWaveComplete())
            {
                return;
            }
            // Send new wave
            // Alternatively call memory game
            NewWave();
        }


        countSinceLast++;

        if (countSinceLast > spawnGap)
        {
            Spawn();
            countSinceLast = 0;
        }

    }

    void NewWave()
    {
        waveId++; // Starts at -1
        wave = waves[waveId];
        countRemaining = new List<int>();
        for(int i = 0; i < wave.typeCount.Count; i++)
        {
            countRemaining.Add(wave.typeCount[i]);
        }

        viruses = new List<GameObject>();
        spawnGap = (int)(wave.spawnInterval / Time.deltaTime);

        player = GameObject.Find("PlayerState");

        player.GetComponent<HealthManager>().playerDeath += GameOver;

        waveRunning = true;
    }

    void Spawn()
    {
        int randIdx = Random.Range(0, countRemaining.Count);

        int i = randIdx;
        while (countRemaining[i] == 0)
        {
            i = (i + 1) % countRemaining.Count;
            if (i == randIdx)
            {
                waveRunning = false;
                return;
            }
        }

        countRemaining[i]--;
        GameObject newVirus = Instantiate(virusPrefabs[i]);

        newVirus.tag = "Virus";

        PathCreator creator = PathManager.GetRandomPath();
        newVirus.transform.position = creator.points[0];
        FollowPath followPath = newVirus.GetComponent<FollowPath>();
        followPath.path = creator;

        VirusStats virusStats = newVirus.GetComponent<VirusStats>();
        virusStats.player = player;

        GameObject healthBar = Instantiate(healthBarPrefab, newVirus.transform);
        UnityEngine.UI.Slider healthSlider = healthBar.GetComponentInChildren<UnityEngine.UI.Slider>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = virusStats.health;
        healthSlider.value = healthSlider.maxValue;

        viruses.Add(newVirus);
    }

    bool CheckWaveComplete()
    {
        for (int i = viruses.Count - 1; i >= 0; i--)
        {
            if (viruses == null)
            {
                viruses.RemoveAt(i);
            }
        }
        return viruses.Count == 0;
    }

    void GameOver()
    {
        gameOver = true;
        waveId = 0;
    }
}
