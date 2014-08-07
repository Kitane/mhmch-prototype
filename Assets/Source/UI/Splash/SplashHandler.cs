using UnityEngine;
using System.Collections;

public class SplashHandler : MonoBehaviour
{	
	void OnPress(bool pressed)
	{
		if (pressed)
		{
			Application.LoadLevel("MainScene");
		}
	}
}
