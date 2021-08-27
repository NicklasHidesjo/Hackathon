using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
	[SerializeField] GameObject InfoScreen;
	CursorChanger cursor;

	private void Start()
	{
		cursor = FindObjectOfType<CursorChanger>();
	}

	private void OnMouseOver()
	{
		InfoScreen.SetActive(true);
		cursor.ChangeCursor(true);
	}
	private void OnMouseExit()
	{
		InfoScreen.SetActive(false);
		cursor.ChangeCursor(false);
	}
}
