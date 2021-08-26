using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoveIsHandler : MonoBehaviour
{
    [SerializeField] GameObject loveIS;

    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI loveIsText;
    [SerializeField] Image loveBubble;
    [SerializeField] GameObject fist;


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

    bool doneRotating;

    [SerializeField] float pitchBlackDuration;
    float pitchBlackTime;
    bool loveIsHappening;

    [SerializeField] float TimeBeforeNextPhase;
    float timePassedAfterRotating;

    [SerializeField] float backgroundFadeDuration;
    float fadeBackgroundTimer;

    [SerializeField] Color backgroundcolor1;
    [SerializeField] Color backgroundcolor2;

    [SerializeField] float rotationDuration;
    float time;
    int currentWord;
    int totalWords;
    int wordCount;


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
        if (!startSpinning) { return; }

        if (doneRotating) 
        {
            timePassedAfterRotating += Time.deltaTime;
            if(timePassedAfterRotating > TimeBeforeNextPhase)
			{

                // make this only set the "what is love" in canvas to non active)
                loveIS.SetActive(false);

                // add a fade out here and then after it's done commence the final act
                
                FindObjectOfType<ApplicationManager>().EnterTheFinalAct();
			}
            return;
        }

        if(wordCount == totalWords -1)
		{
            doneRotating = true;
            timePassedAfterRotating = 0;
            return;
		}

        time += Time.deltaTime / rotationDuration;

        float word = Mathf.Lerp(wordCount, totalWords, time);
        currentWord = (int)word % loveWords.Count;
        wordCount = (int)word;
        //wordCount = Mathf.Clamp(wordCount ,0, totalWords - 3);
        loveWord.text = loveWords[currentWord];
	}

	private void FadeLoveIsText()
	{
        if (!fadeIn) { return; }


        faded = loveBubble.color;
        faded.a = 0;
        nonFaded = loveBubble.color;
        nonFaded.a = 1;
        loveBubble.color = Color.Lerp(faded, nonFaded, fadeTimer);	


        faded = loveIsText.color;
        faded.a = 0;
        nonFaded = loveIsText.color;
        nonFaded.a = 1;

        fadeBackgroundTimer += Time.deltaTime / backgroundFadeDuration;
        background.color = Color.Lerp(backgroundcolor1, backgroundcolor2, fadeBackgroundTimer);


        loveIsText.color = Color.Lerp(faded, nonFaded, fadeTimer);

        loveWord.color = Color.Lerp(faded, nonFaded, fadeTimer);

		
        fadeTimer += Time.deltaTime / fadeInDuration;
        if(fadeTimer > 1 && !fadeInDone)
		{
            fadeInDone = true;
            fist.SetActive(true);
        }
	}

	private void OnMouseDown()
	{
        if(fadeInDone && !startSpinning)
		{
            fist.SetActive(false);
            startSpinning = true;
		}
	}

	public void Initialize()
	{
        totalWords = rotations * loveWords.Count;
        time = 0;
        wordCount = 0;

        loveIS.SetActive(true);

        loveIsHappening = true;
        loveWord.text = loveWordDefault;
        fadeBackgroundTimer = 0;
        fadeIn = true;
	}

    public void ResetValues()
	{
        fadeTimer = 0;
        fadeIn = false;
        fadeInDone = false;

        backgroundFadeDuration = 0;
        fadeBackgroundTimer = 0;

        pitchBlackTime = 0;
        loveIsHappening = false;

        startSpinning = false;
        doneRotating = false;

        timePassedAfterRotating = 0;

        background.color = faded;
        loveIsText.color = faded;
        
        loveWord.color = faded;

        faded = loveBubble.color;
        faded.a = 0;
        loveBubble.color = faded;
    }
}
