using UnityEngine;
using System.Collections;

public class SplashHandler : MonoBehaviour
{	
	GameObject mechPanel;

	void Start()
	{
		mechPanel = GameObject.Find ("MechPanel");
		mechPanel.SetActive(false);

	}
	void OnPress(bool pressed)
	{
		var splashPanel = GameObject.Find("SplashPanel");
		splashPanel.SetActive(false);

		mechPanel.SetActive(true);
	}
}


