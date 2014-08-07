using UnityEngine;
using System.Collections;

public class AbilityHandler : MonoBehaviour
{
	public TeslaShieldAbility _teslaShieldAbility;
	
	void OnAbilitySelection(GameObject gameObject)
	{
		string buttonName = gameObject.name;
		Debug.Log("OnAbilitySelection:" + buttonName);
		
		if (buttonName == "TeslaShieldButton")
		{
			if (_teslaShieldAbility != null)
			{
				if (_teslaShieldAbility.IsReady())
				{
					_teslaShieldAbility.StartAbility();
				}
			}
		}
	}
}
