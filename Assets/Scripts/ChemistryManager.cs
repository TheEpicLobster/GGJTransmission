using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ChemistryManager : MonoBehaviour
{
    [Range(1, 16)]
    public int round = 3;

    [Range(1, 10)]
    public float countdown = 3.0f;

    [Range(1, 10)]
    public int scoreIncrease = 5;

    [Range(1, 10)]
    public int scoreDecrease = 1;

    [Range(1, 20)]
    public int LookAgainCost = 3;
    public List<InputField> Chemicals = new List<InputField>();
    public List<InputField> otherChem = new List<InputField>();
    public InputField secret;
    public Button submit;
    public Button LookAgain;
    public Text scoreText;
    public Text countDown;
    private int score = 0;
    private bool review = false;
    

    public void Submit_Click()
    {

        int enabled = 0;
        foreach (InputField i in Chemicals)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(i.text))
            {

                Debug.Log("Stuff?");
                secret.gameObject.SetActive(true);
            }

            i.text = i.text.ToUpper(); ;
            string name = i.name.Split('(')[0];

            
            if (i.text == name && i.enabled == true)
            {
            
                    score += scoreIncrease;
                    i.enabled = false;
                
            }
            else if (i.text != name)
            {
                
                ColorBlock cb = i.colors;
                cb.normalColor = Color.red;
                i.colors = cb;
                score-= scoreDecrease;
            }
            if (i.enabled == true)
            {
                enabled++;
            }
        }

        if (enabled == 0)
        {
            submit.enabled = false;
            LookAgain.enabled = false;
            score = Mathf.Max(-(round * 5), score);
            ChemistryCalls.VirusHealthMult = 1.0f / Mathf.Lerp(0.5f, 2, ((score / (round * 5))+ 1) / 2);
            Debug.Log(ChemistryCalls.VirusHealthMult);
            ChemistryCalls.towerDefence.SetActive(true);
            try
            {
                SceneManager.UnloadSceneAsync("Chemistry");
            }
            catch ( System.Exception e )
            {
                SceneManager.UnloadSceneAsync("Chem2");
            }
            Destroy(GameObject.Find("ChemistryObject"));
        }

        scoreText.text = "Score: " + score;
    }

    public void easterEgg()
    {
        if(secret.text == "7677")
        {
            countDown.text = "EasterEgg";
        }
    }

    public void ReView()
    {
        score -= LookAgainCost;
        
        scoreText.text = "Score: " + score;

        countdown = 5;
        countDown.text = countdown.ToString();
        foreach (InputField i in Chemicals)
        {
            if(i.enabled == true)
            {
                Debug.Log("re-view");
                i.text = i.name;
                otherChem.Add(i);
            }
        }
    }
	// Use this for initialization
	void Start ()
    {
        if (ChemistryCalls.timesCalled == 1)
        {
            round = 2;
        }
        else
        {
            round = 6;
        }

        foreach(InputField i in Chemicals)
        {
            
            i.text = i.name;
            i.enabled = false;
            
        }
   }
    public void Begin()
    {
        for (int i = 0; i < round; i++)
        {
            int r = Random.Range(0, Chemicals.Count);
            Chemicals[r].text = "";
            //R.text = "R";
            Chemicals[r].enabled = true;

            otherChem.Add(Chemicals[r]);
            Chemicals.Remove(Chemicals[r]);
        }
        foreach (InputField x in otherChem)
        {
            Chemicals.Add(x);
        }
        otherChem.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
                countDown.text = countdown.ToString();
                if (countdown <= 0)
                {
                countDown.text = "";
                if (review == false)
                {
                    Begin();
                    review = true;
                }
                else
                {
                    foreach (InputField x in otherChem)
                    {
                        x.text = "";

                    }
                    otherChem.Clear();
                }
                }
            }
       
    }
}
