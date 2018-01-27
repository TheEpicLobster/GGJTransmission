using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemistryManager : MonoBehaviour
{
    [Range(1, 11)]
    public int round = 3;

    [Range(1, 10)]
    public float countdown = 3.0f;
    public List<InputField> Chemicals = new List<InputField>();
    public List<InputField> otherChem = new List<InputField>();
    //public InputField R;
    //public InputField O1;
    //public InputField O2;
    //public InputField O3;
    //public InputField HN;
    //public InputField N;
    //public InputField H;
    //public InputField S;
    //public InputField OH;
    //public InputField CH31;
    //public InputField CH32;
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
            
            
            string name = i.name.Split('(')[0];

            
            if (i.text == name && i.enabled == true)
            {
                    score++;
                    i.enabled = false;
                    Debug.Log("Score + 1");
                
            }
            else if (i.text != name)
            {
                // i.transition = new Selectable.Transition(new Color(0,0,255));
                // i.colors.normalColor.r = 0;// = new Color(0, 0, 255);
                
                ColorBlock cb = i.colors;
                cb.normalColor = Color.red;
                i.colors = cb;
                score--;
            }
            if (i.enabled == true)
            {
                enabled++;
            }
        }

        if (enabled == 0)
        {
            countDown.text = "end";
            submit.enabled = false;
            LookAgain.enabled = false;
        }



        scoreText.text = "Score: " + score;
    }

    public void ReView()
    {
        score -= 3;
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
        //if (countdown <= 0)
        //{
        //    foreach (InputField x in otherChem)
        //    {
        //        x.text = "";

        //    }
        //    otherChem.Clear();
        //}
    }
	// Use this for initialization
	void Start ()
    {

        //Chemicals.Add(R);
        //Chemicals.Add(O1);
        //Chemicals.Add(O2);
        //Chemicals.Add(O3);
        //Chemicals.Add(HN);
        //Chemicals.Add(N);
        //Chemicals.Add(H);
        //Chemicals.Add(S);
        //Chemicals.Add(OH);
        //Chemicals.Add(CH31);
        //Chemicals.Add(CH32);
        
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
