using UnityEngine;
using System.Collections;

public class MechSelectHandler : MonoBehaviour
{
	public GameManager.MechTypes MechType;

	void OnClick()
	{
		Application.LoadLevel("MainScene");
		GameManager.Instance.MechType = MechType;
	}
}