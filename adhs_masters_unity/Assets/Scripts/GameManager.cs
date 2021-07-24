using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller bs;
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNode = 150;

    public Text scoreText;
    public Text multiplierText;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        { 
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                bs.hasStarted = true;

                theMusic.Play();
            }
        }
    }
    public void noteHit()
    {
        Debug.Log("Great.");
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            // Increase Multiplier tracker for thresholds
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        // Add score
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;

        // Show Multiplier
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }

    public void normalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        noteHit();
    }

    public void goodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        noteHit();
    }

    public void perfectHit()
    {
        currentScore += scorePerPerfectNode * currentMultiplier;
        noteHit();
    }

    public void noteMissed()
    {
        Debug.Log("Miss.");
        // Loose multiplier
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        //owo
    }

}

