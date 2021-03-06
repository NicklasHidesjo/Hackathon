using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ThoughtOverloadHandler : MonoBehaviour
{
	[SerializeField] AudioSource noice;
	[SerializeField] AudioClip sound;
	float soundTimer;

	[SerializeField] float TimeBeforeOverloadStarts;


    [SerializeField] Thought thought;
	[SerializeField] Transform parent;

    [SerializeField] Image background;
	[SerializeField] Color faded;
	[SerializeField] Color nonFaded;

	[SerializeField] float overloadDuration;
    [SerializeField] float startingTimeBetweenThoughts;
    [SerializeField] float decreaseBetweenThoughts;
    [SerializeField] float lowestTimeBetweenThoughts;
	[SerializeField] float betweenThoughtsFadeStartTime;

	[SerializeField] int touchFreeThoughts;
	[SerializeField] float safeDistance;

    [SerializeField] Vector2 minBounds;
    [SerializeField] Vector2 maxBounds;

    List<Thought> thoughts = new List<Thought>();

	[SerializeField] List<string> longThoughts = new List<string>();
	[SerializeField] List<string> shortThoughts = new List<string>();

	float overloadTime = 0;

    float timeSinceLastThought;
    float timeBetweenThoughts;

    bool thoughtOverload;
	float lowestVolume;

    float time;
	ApplicationManager manager;

	public bool ThoughtOverload 
    { 
        get 
        { 
            return thoughtOverload; 
        } 
        set 
        {
            thoughtOverload = value;
			noice.clip = sound;
			noice.loop = true;
			noice.Play();
        } 
    }

	private void Start()
	{
		manager = FindObjectOfType<ApplicationManager>();
		lowestVolume = manager.AulaVolume;
        timeBetweenThoughts = startingTimeBetweenThoughts;
    }


	private void Update()
	{
		if (!thoughtOverload) { return; }
		overloadTime += Time.deltaTime;
		time += Time.deltaTime;
		if(overloadTime < TimeBeforeOverloadStarts) { return; }
		manager.ChangeClickedPerson(null);
		timeSinceLastThought += Time.deltaTime;

		if (time >= overloadDuration)
		{
			background.color = nonFaded;
		}

		soundTimer += Time.deltaTime / overloadDuration;
		if (!manager.Muted)
		{
			noice.volume = Mathf.Lerp(lowestVolume, 1, soundTimer);
		}
		else
		{
			noice.volume = 0;
		}

		if (timeSinceLastThought < timeBetweenThoughts) { return; }
		timeSinceLastThought = 0;

		SpawnNewThought();
		DecreaseTimeBetweenThougts();


		if (time < overloadDuration) { return; }
		ThoughtOverload = false;

		DestroyThoughts();
		FindObjectOfType<ApplicationManager>().TurnOffScreen();
	}

	private void SpawnNewThought()
	{
		Thought newThought = Instantiate(thought);
		newThought.transform.SetParent(parent);

		TextMeshProUGUI text = newThought.Text;

		if (thoughts.Count < touchFreeThoughts)
		{
			do
			{
				SetNewThoughtPosition(newThought);
			}
			while (CheckPosition(newThought));
		
			int random = Random.Range(0, longThoughts.Count);
			text.text = longThoughts[random].ToUpper();
		}
		else
		{
			SetNewThoughtPosition(newThought);

			int random = Random.Range(0, shortThoughts.Count);
			text.text = shortThoughts[random].ToUpper();
		}

		float zRotation = Random.Range(0, 50) * (Random.Range(0, 2) * 2 - 1);
		newThought.transform.rotation = Quaternion.Euler(new Vector3(newThought.transform.rotation.x, newThought.transform.rotation.y, zRotation));

		thoughts.Add(newThought);
	}

	private void SetNewThoughtPosition(Thought newThought)
	{
		float x = Random.Range(minBounds.x, maxBounds.x);
		float y = Random.Range(minBounds.y, maxBounds.y);
		newThought.transform.position = Camera.main.WorldToScreenPoint(new Vector2(x,y));
	}

	private bool CheckPosition(Thought newThought)
	{
		foreach (var thought in thoughts)
		{
			if(Vector2.Distance(thought.transform.position, newThought.transform.position) < safeDistance)
			{
				return true;
			}
		}
		return false;
	}

	private void DecreaseTimeBetweenThougts()
	{
		timeBetweenThoughts -= decreaseBetweenThoughts;
		timeBetweenThoughts = Mathf.Clamp(timeBetweenThoughts, lowestTimeBetweenThoughts, Mathf.Infinity);
	}

	private void DestroyThoughts()
	{
		noice.Stop();
		Thought[] thoughtsToDestroy = new Thought[thoughts.Count];
		for (int i = 0; i < thoughts.Count; i++)
		{
			thoughtsToDestroy[i] = thoughts[i];
		}
		thoughts.Clear();
		foreach (var thought in thoughtsToDestroy)
		{
			Destroy(thought.gameObject);
		}
	}

	public void ResetValues()
	{
		timeSinceLastThought = 0;
		timeBetweenThoughts = startingTimeBetweenThoughts;
		overloadTime = 0;
		thoughtOverload = false;

		time = 0;
		soundTimer = 0;
	}
}
