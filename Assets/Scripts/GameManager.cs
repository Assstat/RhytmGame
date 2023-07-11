using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;

    public int currentMultiplier;
    public int MultiplierTracker;
    public int[] multiplierThresholds;

    public Text ScoreText;
    public Text MultiText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        ScoreText.text = "Score: 0"; 
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                
                theMusic.Play();
            }
        }
    }
    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        MultiplierTracker++;
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            MultiplierTracker++;
           
            if(multiplierThresholds[currentMultiplier -1] <= MultiplierTracker)
            {
                MultiplierTracker = 0;
                currentMultiplier++;
            }
        }
        currentScore += scorePerNote * currentMultiplier;

        MultiText.text = "Multiplier: x" + currentMultiplier;
        ScoreText.text = "Score:" + currentScore;
        
    }
    public void NoteMissed()
    {   
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        MultiplierTracker = 0;
        MultiText.text = "Multiplier: x" + currentMultiplier;
    }
}
