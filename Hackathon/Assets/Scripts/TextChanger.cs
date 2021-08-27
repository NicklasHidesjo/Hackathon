using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
	[SerializeField] Color hoverColor = Color.white;
	[SerializeField] Color nonHoverColor = Color.black;
	[SerializeField] TextMeshProUGUI text;

	public void Hover(bool underline)
	{
		text.color = hoverColor;

		if (underline)
		{
			text.fontStyle = FontStyles.Underline;
		}

	}
	public void StopHovering()
	{
		text.color = nonHoverColor;
		text.fontStyle = FontStyles.Normal;
	}
} 
