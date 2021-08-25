using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheFinalActHandler : MonoBehaviour
{
    [SerializeField] GameObject PlayAgainButton;

    [SerializeField] TextMeshProUGUI FinalMessage;
    [SerializeField] float fadeInDuration;
    [SerializeField] Color faded;
    [SerializeField] Color nonFaded;

    float fadeTimer;
    bool enteredTheFinalAct;
    public void CommenceFinalAct()
	{
        enteredTheFinalAct = true;
	}

    private void Update()
    {
		if (!enteredTheFinalAct) { return; }

        FinalMessage.color = Color.Lerp(faded, nonFaded, fadeTimer);

        fadeTimer += Time.deltaTime / fadeInDuration;

        if(fadeTimer > 1)
		{
            PlayAgainButton.SetActive(true);
		}
    }

	public void ResetValues()
	{
        FinalMessage.color = faded;
        fadeTimer = 0;
        enteredTheFinalAct = false;
        PlayAgainButton.SetActive(false);
	}
}
