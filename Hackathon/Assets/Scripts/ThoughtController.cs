using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtController : MonoBehaviour
{
    [SerializeField] Image bubble1;
    [SerializeField] Image bubble2;
    [SerializeField] Image bubble3;
    [SerializeField] TextMeshProUGUI thoughtText;

    Image body;

	private void Start()
	{
        body = GetComponent<Image>();
	}

	private void Update()
	{
		SetAlpha(bubble1, bubble1.color);
		SetAlpha(bubble2, bubble2.color);
		SetAlpha(bubble3, bubble3.color);
		SetAlpha(thoughtText, thoughtText.color);
	}

	private void SetAlpha(Image image, Color color)
	{
		float a = body.color.a;
		image.color = new Color(color.r, color.g, color.b, a);
	}
	private void SetAlpha(TextMeshProUGUI text, Color color)
	{
		float a = body.color.a;
		text.color = new Color(color.r, color.g, color.b, a);
	}

	public void SetText(string text)
	{
		thoughtText.text = text;
	}
}
