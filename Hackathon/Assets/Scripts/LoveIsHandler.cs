using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoveIsHandler : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] TextMeshProUGUI loveIsText;
    [SerializeField] Image line;

    [SerializeField] float fadeInDuration;
    [SerializeField] Color faded;
    [SerializeField] Color nonFaded;
    float fadeTimer;
    bool fadeIn;
	bool fadeInDone;

    [SerializeField] List<string> loveWords;
    [SerializeField] TextMeshProUGUI loveWord;
    [SerializeField] string loveWordDefault;

    bool startSpinning;
    [SerializeField] int rotations;
    [SerializeField] int endWord;
    [SerializeField] float startingWordDuration;
    [SerializeField] float maxWordDuration;
    [SerializeField] float drag;

    float currentWordDuration;
    int currentWord;
    int currentRotation;
    float timeSinceWordChanged;

    bool lastRotation;
    bool doneRotating;

    [SerializeField] float TimeBeforeNextPhase;
    float timePassedAfterRotating;

    private void Update()
	{
		FadeLoveIsText();
        SpinWords();
	}

	private void SpinWords()
	{
        // make the words first go fast then slow down
        // especially at the last "rotation" so that you can read all the 
        // words of what love is.

        if(!startSpinning) { return; }
		if (doneRotating) 
        {
            timePassedAfterRotating += Time.deltaTime;
            if(timePassedAfterRotating > TimeBeforeNextPhase)
			{                
                loveWord.gameObject.SetActive(false);
                loveIsText.gameObject.SetActive(false);
                line.gameObject.SetActive(false);

                // add a fade out here and then after it's done commence the final act
                
                FindObjectOfType<ApplicationManager>().EnterTheFinalAct();
			}
            return;
        }
        timeSinceWordChanged += Time.deltaTime;
        loveWord.text = loveWords[currentWord];

        if(timeSinceWordChanged > currentWordDuration)
		{
            timeSinceWordChanged = 0;
            currentWordDuration += drag * Time.deltaTime;

            currentWord++;
            if(currentWord >= loveWords.Count)
			{
                currentWord = 0;
                currentRotation++;

                if(lastRotation)
				{
                    doneRotating = true;
                    timePassedAfterRotating = 0;
				}
                if(currentRotation == rotations-1)
				{
                    lastRotation = true;
				}
			}
		}
	}

	private void FadeLoveIsText()
	{
        if (!fadeIn) { return; }
		
        loveIsText.color = Color.Lerp(faded, nonFaded, fadeTimer);

        loveWord.color = Color.Lerp(faded, nonFaded, fadeTimer);

        line.color = Color.Lerp(faded, nonFaded, fadeTimer);	
		
        fadeTimer += Time.deltaTime / fadeInDuration;
        if(fadeTimer > 1)
		{
            fadeInDone = true;
		}
	}

	private void OnMouseDown()
	{
        if(fadeInDone && !startSpinning)
		{
            startSpinning = true;
		}
	}

	public void Initialize()
	{
        loveWord.gameObject.SetActive(true);
        loveIsText.gameObject.SetActive(true);
        line.gameObject.SetActive(true);

        background.SetActive(true);
        loveWord.text = loveWordDefault;
        fadeIn = true;
	}


    public void ResetValues()
	{
        fadeTimer = 0;
        fadeIn = false;
        fadeInDone = false;

        startSpinning = false;

        lastRotation = false;
        doneRotating = false;

        currentWordDuration = startingWordDuration;
        currentWord = 0;
        currentRotation = 0;
        timeSinceWordChanged = 0;

        timePassedAfterRotating = 0;

        background.GetComponent<SpriteRenderer>().color = faded;
        loveIsText.color = faded;
        line.color = faded;
        loveWord.color = faded;
    }

}
