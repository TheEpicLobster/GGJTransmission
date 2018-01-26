using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIUpdater : MonoBehaviour {

    public HealthManager manager;

    UnityEngine.UI.Text text;

	// Use this for initialization
	void Awake () {
        manager.healthUpdated += LoseHealth;
        text = GetComponent<UnityEngine.UI.Text>();
	}

    private void Start()
    {
        text.text = manager.health.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void LoseHealth(int newHealth)
    {
        text.text = newHealth.ToString();
    }
}
