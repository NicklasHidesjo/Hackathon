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

    bool startSpinning;
    [SerializeField] int rotations;
    [SerializeField] int endWord;
    [SerializeField] float wordDuration;
    [SerializeField] float drag;

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
        }
        timeSinceWordChanged += Time.deltaTime;
        loveWord.text = loveWords[currentWord];


        if(timeSinceWordChanged > wordDuration)
		{
            timeSinceWordChanged = 0;
            currentWord++;
            if(currentWord >= loveWords.Count)
			{
                currentWord = 0;
                currentRotation++;

                if(lastRotation)
				{
                    doneRotating = true;
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
		float x = Mathf.Lerp(0, 1, fadeTimer * 2);
		float y = Mathf.Lerp(0, 1, fadeTimer * 2);
		loveIsText.rectTransform.localScale = new Vector3(x, y, 1);
        loveWord.color = Color.Lerp(faded, nonFaded, fadeTimer);
        loveWord.rectTransform.localScale = new Vector3(x, y, 1);
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
        background.SetActive(true);
;
        fadeIn = true;
	}
}
