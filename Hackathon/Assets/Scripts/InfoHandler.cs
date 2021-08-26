using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
	[SerializeField] GameObject InfoScreen;

	private void OnMouseOver()
	{
		InfoScreen.SetActive(true);
	}
	private void OnMouseExit()
	{
		InfoScreen.SetActive(false);
	}
}
