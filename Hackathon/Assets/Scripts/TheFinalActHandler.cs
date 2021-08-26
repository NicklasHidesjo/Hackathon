using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheFinalActHandler : MonoBehaviour
{
    [SerializeField] GameObject PlayAgainButton;
    [SerializeField] GameObject AboutButton;

    [SerializeField] TextMeshProUGUI finalMessagePartOne;
    [SerializeField] TextMeshProUGUI finalMessagePartTwo;
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
        faded = finalMessagePartOne.color;
        faded.a = 0;
        nonFaded = finalMessagePartOne.color;
        nonFaded.a = 1;
        finalMessagePartOne.color = Color.Lerp(faded, nonFaded, fadeTimer);

        faded = finalMessagePartTwo.color;
        faded.a = 0;
        nonFaded = finalMessagePartTwo.color;
        nonFaded.a = 1;
        finalMessagePartTwo.color = Color.Lerp(faded, nonFaded, fadeTimer);

        fadeTimer += Time.deltaTime / fadeInDuration;

        if(fadeTimer > 1)
		{
            PlayAgainButton.SetActive(true);
            AboutButton.SetActive(true);
		}
    }

	public void ResetValues()
	{
        finalMessagePartOne.color = faded;
        finalMessagePartTwo.color = faded;
        fadeTimer = 0;
        enteredTheFinalAct = false;
        PlayAgainButton.SetActive(false);
        AboutButton.SetActive(false);
	}
}
