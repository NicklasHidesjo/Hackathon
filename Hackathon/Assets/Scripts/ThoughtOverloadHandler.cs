using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtOverloadHandler : MonoBehaviour
{
    [SerializeField] GameObject thought;
    [SerializeField] float overloadDuration;
    [SerializeField] float startingTimeBetweenThoughts;
    [SerializeField] float decreaseBetweenThoughts;
    [SerializeField] float lowestTimeBetweenThoughts;

	[SerializeField] int touchFreeThoughts;

    [SerializeField] Vector2 minBounds;
    [SerializeField] Vector2 maxBounds;

    List<GameObject> thoughts = new List<GameObject>();

    float timeSinceLastThought;
    float timeBetweenThoughts;

    bool thoughtOverload;

    float time;
    
    public bool ThoughtOverload 
    { 
        get 
        { 
            return thoughtOverload; 
        } 
        set 
        {
            thoughtOverload = value; 
        } 
    }

	private void Start()
	{
        timeBetweenThoughts = startingTimeBetweenThoughts;
    }


	private void Update()
	{
		if (!thoughtOverload) { return; }

		timeSinceLastThought += Time.deltaTime;

		if (timeSinceLastThought < timeBetweenThoughts) { return; }
		timeSinceLastThought = 0;

		SpawnNewThought();

		DecreaseTimeBetweenThougts();

		time += Time.deltaTime;
		if (time < overloadDuration) { return; }

		ThoughtOverload = false;
		DestroyThoughts();
		FindObjectOfType<ApplicationManager>().TurnOffScreen();
	}

	private void SpawnNewThought()
	{
		GameObject newThought = Instantiate(thought);

		if(thoughts.Count < touchFreeThoughts)
		{
			do
			{
				SetNewThoughtPosition(newThought);
			}
			while (CheckPosition(newThought));
		}
		else
		{
			SetNewThoughtPosition(newThought);
		}

		float zRotation = Random.Range(0, 50) * (Random.Range(0, 2) * 2 - 1);
		newThought.transform.rotation = Quaternion.Euler(new Vector3(newThought.transform.rotation.x, newThought.transform.rotation.y, zRotation));

		thoughts.Add(newThought);
	}

	private void SetNewThoughtPosition(GameObject newThought)
	{
		float x = Random.Range(minBounds.x, maxBounds.x);
		float y = Random.Range(minBounds.y, maxBounds.y);
		newThought.transform.position = new Vector2(x, y);
	}

	private bool CheckPosition(GameObject newThought)
	{
		foreach (var thought in thoughts)
		{
			if(Vector2.Distance(thought.transform.position, newThought.transform.position) < 2f)
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
		GameObject[] thoughtsToDestroy = new GameObject[thoughts.Count];
		for (int i = 0; i < thoughts.Count; i++)
		{
			thoughtsToDestroy[i] = thoughts[i];
		}
		thoughts.Clear();
		foreach (var thought in thoughtsToDestroy)
		{
			Destroy(thought);
		}
	}
}
