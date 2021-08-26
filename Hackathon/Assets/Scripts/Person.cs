using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
	[SerializeField] ThoughtController thoughtBubble;

	[SerializeField] Sprite blankExpression;
	[SerializeField] Sprite nonBlankExpression;

	[SerializeField] List<string> thoughts = new List<string>();
	int lastThought;

	[SerializeField] float fadeDuration = 1f;
	[SerializeField] Color faded;
	[SerializeField] Color nonFaded;
	float fadeTimer;
	bool fadeIn;
	bool interactedWith;

	Image thoughtRenderer;
	ApplicationManager manager;

	SpriteRenderer face;

	private void Start()
	{		
		fadeIn = false;
		fadeTimer = 0;
		
		thoughtRenderer = thoughtBubble.GetComponent<Image>();
		manager = FindObjectOfType<ApplicationManager>();
		face = GetComponent<SpriteRenderer>();
		
		face.sprite = blankExpression;
	}

	private void OnMouseOver()
	{
		face.sprite = nonBlankExpression;
	}

	private void OnMouseDown()
	{
		if (manager.OverloadStarted) { return; }

		if (!fadeIn)
		{
			int random;
			do
			{
				random = Random.Range(0, thoughts.Count);
			}
			while (random == lastThought);
			thoughtBubble.SetText(thoughts[random]);
			lastThought = random;
		}

		fadeIn = true;
		if (interactedWith){ return; }
		interactedWith = true;
		manager.IncreaseInteraction();
	}

	private void OnMouseExit()
	{
		fadeIn = false;
		face.sprite = blankExpression;
	}

	private void Update()
	{
		thoughtRenderer.color = Color.Lerp(faded, nonFaded, fadeTimer);
		if (fadeIn)
		{
			fadeTimer += Time.deltaTime / fadeDuration;
		}
		else
		{
			fadeTimer -= Time.deltaTime / fadeDuration;
		}
		fadeTimer = Mathf.Clamp(fadeTimer, 0, 1);

/*		if(!fadeOut) { return; }
		textTimer += Time.deltaTime;
		if (textTimer > textDuration)
		{
			fadeOut = false;
			fadeIn = false;
		}*/
	}


	public void ResetPerson()
	{
		interactedWith = false;
	}
}
