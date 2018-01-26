using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemistryManager : MonoBehaviour
{
    static int round = 3;
    public List<InputField> Chemicals = new List<InputField>();
    public InputField R;
    public InputField O1;
    public InputField O2;
    public InputField O3;
    public InputField HN;
    public InputField N;
    public InputField H;
    public InputField S;
    public InputField OH;
    public InputField CH31;
    public InputField CH32;
    public Text scoreText;
    public Text countDown;
    private float countdown = 3.0f;
    private int score = 0;

    

    public void Submit_Click()
    {

        foreach(InputField i in Chemicals)
        {
            if(i.text == i.name)
            {
                score++;
                Debug.Log(i.text +" " + i.name);
            }
        }

        //if(R.text != "R")
        //{
        //    Debug.Log("R not " + R.text);
        //}
        //else if()
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (O1.text != "O")
        //{
        //    Debug.Log("O not " + O1.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (O2.text != "O")
        //{
        //    Debug.Log("O not " + O2.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (O3.text != "O")
        //{
        //    Debug.Log("O not " + O3.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (HN.text != "HN")
        //{
        //    Debug.Log("HN not " + HN.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (N.text != "N")
        //{
        //    Debug.Log("N not " + N.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (H.text != "H")
        //{
        //    Debug.Log("H not " + H.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (S.text != "S")
        //{
        //    Debug.Log("S not " + S.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (OH.text != "OH")
        //{
        //    Debug.Log("OH not " + OH.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (CH31.text != "CH3")
        //{
        //    Debug.Log("CH3 not " + CH31.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}

        //if (CH32.text != "CH3")
        //{
        //    Debug.Log("CH3 not " + CH32.text);
        //}
        //else
        //{
        //    score++;
        //    Debug.Log("Correct");
        //}


        scoreText.text = "Score: " + score;
    }

	// Use this for initialization
	void Start ()
    {

        Chemicals.Add(R);
        Chemicals.Add(O1);
        Chemicals.Add(O2);
        Chemicals.Add(O3);
        Chemicals.Add(HN);
        Chemicals.Add(N);
        Chemicals.Add(H);
        Chemicals.Add(S);
        Chemicals.Add(OH);
        Chemicals.Add(CH31);
        Chemicals.Add(CH32);
        

        
        foreach(InputField i in Chemicals)
        {
            
            i.text = i.name;
            //R.text = "R";
            i.readOnly = true;
            
        }
       

        
        
   }
    public void Begin()
    {
        countDown.text = "";
        for (int i = 0; i < round; i++)
        {
            int r = Random.Range(0, Chemicals.Count);
            Chemicals[r].text = "";
            //R.text = "R";
            Chemicals[r].readOnly = false;

            //Chemicals.Remove(Chemicals[r]);
            
        }
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
                
                    Begin();
                }
            }
       
    }
}
