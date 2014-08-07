using UnityEngine;
using System.Collections;

public class WeaponsHandler : MonoBehaviour
{
	void OnWeaponSelection(GameObject gameObject)
	{
		Debug.Log("OnWeaponSelection:" + gameObject.name);
	}
}
