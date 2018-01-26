using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public enum State
    {
        GameOver,
        Victory,
        InProgress,
        Restarting
    }

    public State gameState { private set; get;}

    System.Action<State> stateChange;

	void Awake () {
        gameObject.GetComponent<HealthManager>().playerDeath += GameOver;
        gameState = State.InProgress;

    }
	
	// Update is called once per frame
	void Update () {
        if (gameState == State.GameOver)
        {
            if (GameObject.FindGameObjectsWithTag("Virus").Length == 0)
            {
                gameState = State.Restarting;
                CallStateChange();
            }
        }
	}


    void GameOver()
    {
        gameState = State.GameOver;
        CallStateChange();
    }

    void CallStateChange()
    {
        if (stateChange != null)
        {
            stateChange.Invoke(gameState);
        }

        Debug.Log("State change to: " + gameState.ToString());
    }
}
