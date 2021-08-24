using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCreator : MonoBehaviour
{
    Person[] people;

    [SerializeField] TextHandler textPrefab;

    private void Start()
    {
        people = FindObjectsOfType<Person>();
        Debug.Log(people.Length);

		foreach (var person in people)
		{
            TextHandler newText = Instantiate(textPrefab);
            newText.transform.SetParent(transform);
            newText.Target = person.ThoughtBubble;
            newText.Initiate(person.ThoughtText);
        }
    }
}
