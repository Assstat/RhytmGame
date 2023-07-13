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
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int MultiplierTracker;
    public int[] multiplierThresholds;

    public Text ScoreText;
    public Text MultiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodText, perfectText, missesText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;

        ScoreText.text = "Score: 0";    
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        
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
        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodText.text = "" + goodHits.ToString();
                perfectText.text = "" + perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";
                if(percentHit > 40)
                {
                    rankVal = "D";
                    if(percentHit > 55)
                    {

                        rankVal = "C";
                        if(percentHit > 70)
                        {

                            rankVal = "B";
                            if (percentHit > 85)
                            {

                                rankVal = "A";
                                if(percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                        
                    }
                }

                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();
            }
        }
    }
    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            MultiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= MultiplierTracker)
            {
                MultiplierTracker = 0;
                currentMultiplier++;
            }
        }

        MultiText.text = "Multiplier :x" + currentMultiplier;

        /*currentScore += scorePerNote * currentMultiplier;*/
        ScoreText.text = "Score:" + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }


    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        MultiplierTracker = 0;

        MultiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;
    }
}
