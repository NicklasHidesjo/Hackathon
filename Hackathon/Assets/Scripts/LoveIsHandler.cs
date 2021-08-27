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


    [SerializeField] float spinnDelay;
    float spinnDelayPassed;

    [SerializeField] float fadeInDuration;
    [SerializeField] Color faded;
    [SerializeField] Color nonFaded;
    float fadeTimer;
    bool fadeIn;
	bool fadeInDone;

    [SerializeField] List<string> loveWords;
    [SerializeField] TextMeshProUGUI loveWord;
    [SerializeField] float fontIncrease;

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
    float word;
    int previousWord;


    [SerializeField] AudioClip tickSound;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] float volume;



    ApplicationManager manager;

	private void Start()
	{
        manager = FindObjectOfType<ApplicationManager>();
	}

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
        if (!fadeInDone) { return; }
        spinnDelayPassed += Time.deltaTime;
        if(spinnDelayPassed < spinnDelay) { return; }

        if (doneRotating) 
        {
            timePassedAfterRotating += Time.deltaTime;
            if(timePassedAfterRotating > TimeBeforeNextPhase)
			{
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

        word = Mathf.Lerp(wordCount, totalWords, time);
        currentWord = (int)word % loveWords.Count;
        wordCount = (int)word;
        //wordCount = Mathf.Clamp(wordCount ,0, totalWords - 3);

        if(currentWord != previousWord && !manager.Muted)
		{
            audioPlayer.clip = tickSound;
            audioPlayer.loop = false;
            audioPlayer.volume = volume;
            audioPlayer.Play();
		}

        if (wordCount == totalWords - 1)
        {
            loveWord.fontStyle = FontStyles.Bold;
            float size = loveWord.fontSize;
            size += fontIncrease;
            loveWord.fontSizeMax = 1000;
        }

        loveWord.text = loveWords[currentWord];

        previousWord = currentWord;
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
        }
	}

	public void Initialize()
	{
        totalWords = rotations * loveWords.Count;
        time = 0;
        wordCount = 0;

        loveIS.SetActive(true);

        loveIsHappening = true;
        loveWord.text = loveWords[0];
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
