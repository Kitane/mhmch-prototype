using UnityEngine;
using System.Collections;

public class WeaponsHandler : MonoBehaviour
{
	public ActorScript _player;

	public UISwitchButton _cannonSwitchButton;
	public UISwitchButton _laserSwitchButton;
	public UISwitchButton _missiledSwitchButton;
	public UISwitchButton _grenadeSwitchButton;

	void Start()
	{
		if (_player != null)
		{
			if (_cannonSwitchButton != null)
			{
				_cannonSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Projectile);
			}

			if (_laserSwitchButton != null)
			{
				_laserSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Energy);
			}

			if (_missiledSwitchButton != null)
			{
				_missiledSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Missile);
			}

			if (_grenadeSwitchButton != null)
			{
				_grenadeSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Grenade);
			}
		}
	}

	void OnWeaponSelection(GameObject gameObject)
	{
		string buttonName = gameObject.name;
		Debug.Log("OnWeaponSelection:" + buttonName);

		UISwitchButton uiSwitchButton = gameObject.GetComponent<UISwitchButton>();
		WeaponScript.WeaponClasses weaponType = WeaponScript.WeaponClasses.Projectile;

		if (buttonName == "CannonButton")
		{
			weaponType = WeaponScript.WeaponClasses.Projectile;
		}
		else if (buttonName == "LaserButton")
		{
			weaponType = WeaponScript.WeaponClasses.Energy;
		}
		else if (buttonName == "MissilesButton")
		{
			weaponType = WeaponScript.WeaponClasses.Missile;
		}
		else if (buttonName == "GrenadesButton")
		{
			weaponType = WeaponScript.WeaponClasses.Grenade;
		}

		EnableWeapon(weaponType, uiSwitchButton.SwitchState);
	}

	private void EnableWeapon(WeaponScript.WeaponClasses weaponType, bool enabled)
	{
		if (_player == null)
		{
			return;
		}

		foreach(WeaponScript weapon in _player.Weapons)
		{
			if (weapon.WeaponClass == weaponType)
			{
				weapon.enabled = enabled;
				break;
			}
		}
	}

	private bool IsWeaponEnabled(WeaponScript.WeaponClasses weaponType)
	{
		if (_player == null)
		{
			return false;
		}
		
		foreach(WeaponScript weapon in _player.Weapons)
		{
			if (weapon.WeaponClass == weaponType)
			{
				return weapon.enabled;
			}
		}

		return false;
	}
}
