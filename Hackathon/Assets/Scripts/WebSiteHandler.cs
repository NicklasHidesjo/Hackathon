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
		Reload();
	}
	public void GoToAbout()
	{
		Application.OpenURL("https://mellanraderna.vercel.app/om-projektet");
	}
	public void GoToRaddningsMissionen()
	{
		//https://gava.raddningsmissionen.se/pfs/donation/dar_det_behovs_bast?interval=recurringlock&grouplock=1
		//https://raddningsmissionen.se/omoss
		Application.OpenURL("https://raddningsmissionen.se/gava");
	}
}
