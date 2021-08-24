using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    GameObject target;
    public GameObject Target { get { return target; } set { target = value; } }
    TextMeshProUGUI text;
    SpriteRenderer bubble;

    [SerializeField] Color textColor;


	public void Initiate(string text)
	{
        this.text = GetComponent<TextMeshProUGUI>();
        bubble = target.GetComponent<SpriteRenderer>();
        this.text.text = text;
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
	}

	private void Update()
    {
        textColor.a = bubble.material.color.a;

        text.color = textColor;
    }
}
