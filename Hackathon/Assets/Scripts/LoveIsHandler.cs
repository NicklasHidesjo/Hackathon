using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoveIsHandler : MonoBehaviour
{
    [SerializeField] Image background;
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
    [SerializeField] float lastRotationWordDuration;
    float currentWordDuration;
    int currentWord;
    int currentRotation;
    float timeSinceWordChanged;

    bool lastRotation;
    bool doneRotating;

    [SerializeField] float pitchBlackDuration;
    float pitchBlackTime;
    bool loveIsHappening;

    [SerializeField] float TimeBeforeNextPhase;
    float timePassedAfterRotating;

    [SerializeField] float rotationDuration;
    float rotationTimer;

    [SerializeField] Color backgroundcolor1;
    [SerializeField] Color backgroundcolor2;

    private void Update()
	{
        if(!loveIsHappening) { return; }
        pitchBlackTime += Time.deltaTime;
        if(pitchBlackTime < pitchBlackDuration) { return; }
		FadeLoveIsText();
        SpinWords();
	}

	private void SpinWords()
	{
        if(!startSpinning) { return; }
        Debug.Log(rotationTimer);
        rotationTimer += Time.deltaTime / rotationDuration;
        background.color = Color.Lerp(backgroundcolor1, backgroundcolor2, rotationTimer);

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
                    currentWordDuration = lastRotationWordDuration;
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
        loveIsHappening = true;
        loveWord.text = loveWordDefault;
        rotationTimer = 0;
        fadeIn = true;
	}

    public void ResetValues()
	{
        fadeTimer = 0;
        fadeIn = false;
        fadeInDone = false;

        rotationDuration = 0;
        rotationTimer = 0;

        pitchBlackTime = 0;
        loveIsHappening = false;

        startSpinning = false;

        lastRotation = false;
        doneRotating = false;

        currentWordDuration = startingWordDuration;
        currentWord = 0;
        currentRotation = 0;
        timeSinceWordChanged = 0;

        timePassedAfterRotating = 0;

        background.color = faded;
        loveIsText.color = faded;
        line.color = faded;
        loveWord.color = faded;
    }
}
