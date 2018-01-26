using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour {
    public List<GameObject> virusPrefabs;
    [Range(0, 10)]
    public float spawnInterval;

    int countSinceLast;
    int spawnGap;

    List<GameObject> viruses;

    GameObject player;


    public GameObject healthBarPrefab;

    bool gameOver = false;

    // Use this for initialization
    void Start () {
        viruses = new List<GameObject>();
        spawnGap = (int)(spawnInterval / Time.deltaTime);

        player = GameObject.Find("PlayerState");
        Spawn();

        player.GetComponent<HealthManager>().playerDeath += GameOver;
	}
	
	// Update is called once per frame
	void Update () {

        if (gameOver)
        {
            return;
        }

        countSinceLast++;

        if (countSinceLast > spawnGap)
        {
            Spawn();
            countSinceLast = 0;
        }

    }

    void Spawn()
    {
        int randIdx = Random.Range(0, virusPrefabs.Count);

        GameObject newVirus = Instantiate(virusPrefabs[randIdx]);

        newVirus.tag = "Virus";

        PathCreator creator = PathManager.GetRandomPath();
        newVirus.transform.position = creator.points[0];
        FollowPath followPath = newVirus.GetComponent<FollowPath>();
        followPath.path = creator;

        VirusStats virusStats = newVirus.GetComponent<VirusStats>();
        virusStats.player = player.GetComponent<HealthManager>();

        GameObject healthBar = Instantiate(healthBarPrefab, newVirus.transform);
        UnityEngine.UI.Slider healthSlider = healthBar.GetComponentInChildren<UnityEngine.UI.Slider>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = virusStats.health;
        healthSlider.value = healthSlider.maxValue;

        viruses.Add(newVirus);
    }

    void GameOver()
    {
        gameOver = true;
    }
}
