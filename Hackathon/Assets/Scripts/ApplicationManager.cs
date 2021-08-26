using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
	[SerializeField] int interactionThreshold;
	[SerializeField] TextMeshProUGUI clickedOn;
	[SerializeField] GameObject informationWindow;
	[SerializeField] GameObject informationHandler;

	int currentInteractions;
	bool overloadStarted;
	public bool OverloadStarted => overloadStarted;

	private void Start()
	{
		currentInteractions = 0;
		clickedOn.text = currentInteractions + "/" + interactionThreshold;
		clickedOn.gameObject.SetActive(true);

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
		}
	}

	public void TurnOffScreen()
	{
		FindObjectOfType<LoveIsHandler>().Initialize();
	}

	public void EnterTheFinalAct()
	{
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
	}
}
