using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
	[SerializeField] int interactionThreshold;

	int currentInteractions;
	bool overloadStarted;
	public bool OverloadStarted => overloadStarted;

	public void IncreaseInteraction()
	{
		if (overloadStarted) { return; }
		currentInteractions++;

		if(currentInteractions >= interactionThreshold)
		{
			FindObjectOfType<ThoughtOverloadHandler>().ThoughtOverload = true;
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
		overloadStarted = false;
	}
}
