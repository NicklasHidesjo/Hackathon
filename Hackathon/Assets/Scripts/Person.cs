using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
	[SerializeField] GameObject thoughtBubble;
	public GameObject ThoughtBubble { get { return thoughtBubble; } }

	[SerializeField] [TextArea(5, 20)] string thoughtText = "";
	public string ThoughtText { get { return thoughtText; } }


	[SerializeField] float fadeDuration = 1f;
	[SerializeField] Color faded;
	[SerializeField] Color nonFaded;
	float fadeTimer;
	bool fadeIn;
	bool fadeOut;

	bool interactedWith;

	[SerializeField] float textDuration = 2f;
	float textTimer;


	SpriteRenderer thoughtRenderer;
	ApplicationManager manager;

	private void Start()
	{		
		fadeIn = false;
		fadeTimer = 0;
		
		thoughtRenderer = thoughtBubble.GetComponent<SpriteRenderer>();
		manager = FindObjectOfType<ApplicationManager>();
	}

	private void OnMouseOver()
	{
		fadeIn = true;
		textTimer = 0;

		if (interactedWith)
		{ return; }
		interactedWith = true;
		manager.IncreaseInteraction();
	}

/*	private void OnMouseDown()
	{
		fadeIn = !fadeIn;
		textTimer = 0;

		if (interactedWith){ return; }
		interactedWith = true;
		manager.IncreaseInteraction();
	}*/

	private void OnMouseExit()
	{
		fadeOut = true;;
	}

	private void Update()
	{
		thoughtRenderer.material.color = Color.Lerp(faded, nonFaded, fadeTimer);
		if (fadeIn)
		{
			fadeTimer += Time.deltaTime / fadeDuration;
		}
		else
		{
			fadeTimer -= Time.deltaTime / fadeDuration;
		}
		fadeTimer = Mathf.Clamp(fadeTimer, 0, 1);

		if(!fadeOut) { return; }
		textTimer += Time.deltaTime;
		if (textTimer > textDuration)
		{
			fadeOut = false;
			fadeIn = false;
		}
	}
}
