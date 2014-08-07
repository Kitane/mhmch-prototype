using UnityEngine;
using System.Collections;

public class WeaponsHandler : MonoBehaviour
{
	public TeslaShieldAbility _teslaShieldAbility;

	void OnWeaponSelection(GameObject gameObject)
	{
		string buttonName = gameObject.name;
		Debug.Log("OnWeaponSelection:" + buttonName);

		if (buttonName == "CannonButton")
		{

		}
		else if (buttonName == "LaserButton")
		{
			
		}
		else if (buttonName == "MissilesButton")
		{
			
		}
		else if (buttonName == "GrenadesButton")
		{
			
		}
	}
}
