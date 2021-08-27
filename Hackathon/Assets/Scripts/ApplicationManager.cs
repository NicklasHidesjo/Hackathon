using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
	[SerializeField] int interactionThreshold;
	[SerializeField] TextMeshProUGUI clickedOn;
	[SerializeField] GameObject informationWindow;
	[SerializeField] GameObject informationHandler;

	[SerializeField] GameObject firstInformationWindow;

	[SerializeField] Button muteButton;
	[SerializeField] Sprite mutedSprite;
	[SerializeField] Sprite unMutedSprite;

	Person lastClickedPerson;

	int currentInteractions;
	bool overloadStarted;
	public bool OverloadStarted => overloadStarted;

	bool started;
	public bool Started => started;

	bool inAula;

	bool muted;
	public bool Muted => muted;

	[SerializeField] float aulaVolume;
	public float AulaVolume => aulaVolume;
	AudioSource audioPlayer;


	[SerializeField] GameObject people;

	private void Start()
	{
		currentInteractions = 0;
		clickedOn.text = currentInteractions + "/" + interactionThreshold;
		clickedOn.gameObject.SetActive(true);
		audioPlayer = FindObjectOfType<AudioSource>();
		audioPlayer.volume = 0;
		inAula = true;
	}

	public void IncreaseInteraction()
	{
		if (overloadStarted) { return; }
		currentInteractions++;

		clickedOn.text = currentInteractions + "/" + interactionThreshold;

		if(currentInteractions >= interactionThreshold)
		{
			FindObjectOfType<ThoughtOverloadHandler>().ThoughtOverload = true;
			clickedOn.gameObject.SetActive(false);
			informationHandler.SetActive(false);
			informationWindow.SetActive(false);
			overloadStarted = true;
			inAula = false;
		}
	}

	public void TurnOffScreen()
	{
		FindObjectOfType<LoveIsHandler>().Initialize();
		FindObjectOfType<CursorChanger>().ChangeCursor(false);
		people.SetActive(false);
	}

	public void EnterTheFinalAct()
	{
		muteButton.gameObject.SetActive(false);
		GetComponent<TheFinalActHandler>().CommenceFinalAct();
	}

	public void PlayAgain()
	{
		FindObjectOfType<ThoughtOverloadHandler>().ResetValues();
		FindObjectOfType<LoveIsHandler>().ResetValues();
		GetComponent<TheFinalActHandler>().ResetValues();
		foreach (var person in FindObjectsOfType<Person>())
		{
			person.ResetPerson();
		}
		currentInteractions = 0;
		clickedOn.text = currentInteractions + "/" + interactionThreshold;
		clickedOn.gameObject.SetActive(true);
		informationWindow.SetActive(true);
		informationHandler.SetActive(true);
		overloadStarted = false;
		started = false;
	}

	public void Mute()
	{
		muted = !muted;
		if(muted)
		{
			if(inAula)
			{
				audioPlayer.volume = 0;
			}
			muteButton.image.sprite = mutedSprite;
		}
		else
		{
			if(inAula && started)
			{
				audioPlayer.volume = aulaVolume;
			}
			muteButton.image.sprite = unMutedSprite;
		}
	}

	public void ChangeClickedPerson(Person person)
	{
		if(lastClickedPerson != null)
		{
			lastClickedPerson.FadeIn = false;
		}
		lastClickedPerson = person;
	}

	public void SetStarted()
	{
		firstInformationWindow.SetActive(false);
		started = true;
		if(!muted)
		{
			audioPlayer.volume = aulaVolume;
		}
	}
}
