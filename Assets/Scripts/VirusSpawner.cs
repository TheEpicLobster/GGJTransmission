using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour {
    public GameObject virusPrefab;
    [Range(0, 10)]
    public float spawnInterval;

    int countSinceLast;
    int spawnGap;

    List<GameObject> viruses;

	// Use this for initialization
	void Start () {
        viruses = new List<GameObject>();
        spawnGap = (int)(spawnInterval / Time.deltaTime);
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
        countSinceLast++;

        if (countSinceLast > spawnGap)
        {
            Spawn();
            countSinceLast = 0;
        }

    }

    void Spawn()
    {
        GameObject newVirus = Instantiate(virusPrefab);

        PathCreator creator = PathManager.GetRandomPath();
        newVirus.transform.position = creator.points[0];
        FollowPath followPath = newVirus.GetComponent<FollowPath>();
        followPath.path = creator;

        viruses.Add(newVirus);
    }
}
