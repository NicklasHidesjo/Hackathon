using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebSiteHandler : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void Reload();

	public void ReloadPage()
	{
		//Reload();
	}
	public void GoToAbout()
	{
		Application.OpenURL("https://mellanraderna.vercel.app/om-projektet");
	}
}
