using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;


	public void ChangeCursor(bool hand)
	{
		if(hand)
		{
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		}
		else
		{ 
			Cursor.SetCursor(null, hotSpot, cursorMode);
		}
	}
}
