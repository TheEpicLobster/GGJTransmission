using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour {
    public List<GameObject> virusPrefabs;

    public GameObject healthBarPrefab;

    public System.Action<int, Wave> newWave;
    public System.Action<int, Wave> waveComplete;

    [System.Serializable]
    public class Wave
    {
        public List<int> typeCount;
        [Range(0.1f, 10)]
        public float spawnInterval;
        public int bonus = 100;
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
    bool waitingOnRestart = true;

    private void Start()
    {
        player = GameObject.Find("PlayerState");
        player.GetComponent<HealthManager>().playerDeath += GameOver;
        viruses = new List<GameObject>();
        waveRunning = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update () {

        if (gameOver)
        {
            return;
        }

        if (!waveRunning)
        {
            if (!CheckWaveComplete() || waitingOnRestart)
            {
                return;
            }

            waitingOnRestart = true;

            if (waveComplete != null)
            {
                waveComplete.Invoke(waveId + 1, wave);
            }
            return;
        }

        countSinceLast++;

        if (countSinceLast > spawnGap)
        {
            Spawn();
            countSinceLast = 0;
        }

    }

    public void NewWave()
    {
        waveId++; // Starts at -1
        wave = waves[waveId];
        countRemaining = new List<int>();
        for(int i = 0; i < wave.typeCount.Count; i++)
        {
            countRemaining.Add(wave.typeCount[i]);
        }
        spawnGap = (int)(wave.spawnInterval / Time.deltaTime);

        waveRunning = true;
        waitingOnRestart = false;

        if ( newWave != null )
        {
            newWave.Invoke(waveId + 1, wave);
        }
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
            if (viruses[i] == null)
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
