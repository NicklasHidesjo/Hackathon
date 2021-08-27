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
	public bool FadeIn { get { return fadeIn; } set { fadeIn = value; } }

	bool interactedWith;
	bool hasPressed;

	[SerializeField] Vector3 hoverScale;

	Image thoughtRenderer;
	ApplicationManager manager;

	SpriteRenderer face;
	CursorChanger cursor;

	private void Start()
	{		
		fadeIn = false;
		fadeTimer = 0;
		
		thoughtRenderer = thoughtBubble.GetComponent<Image>();
		manager = FindObjectOfType<ApplicationManager>();
		face = GetComponent<SpriteRenderer>();
		cursor = FindObjectOfType<CursorChanger>();
		
		face.sprite = blankExpression;
	}

	private void OnMouseOver()
	{
		if(!manager.Started) { return; }
		cursor.ChangeCursor(true);
		face.sprite = nonBlankExpression;
		face.transform.localScale = hoverScale;
	}

	private void OnMouseDown()
	{
		if(!manager.Started) { return; }
		manager.ChangeClickedPerson(this);
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
		hasPressed = true;
	}

	private void OnMouseExit()
	{
		if(!manager.Started) { return; }
		cursor.ChangeCursor(false);
		//fadeIn = false;
		face.sprite = blankExpression;
		face.transform.localScale = new Vector3(1, 1, 1);

		if (!hasPressed || interactedWith)
		{ return; }
		interactedWith = true;
		manager.IncreaseInteraction();
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
	}


	public void ResetPerson()
	{
		interactedWith = false;
		hasPressed = false;
	}
}
