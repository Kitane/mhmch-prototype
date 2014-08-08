using UnityEngine;
using System.Collections;

public class TouchCatch : MonoBehaviour {

	public void OnPress (bool isPressed) 
	{ 
		if (isPressed)
		{
			Debug.Log ("Touch catch:");
		}
	}
}
