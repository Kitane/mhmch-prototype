using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public float NativeWidth = 720;
	public float NativeHeight = 1280;

	public float DynamicWidth;
	public float DynamicHeight;

	public GUIStyle InfoHUDStyle;
	public GUIStyle ActionBarStyle;

	public UISlider _steamBar;
	public UISlider _healthBar;

	public UISwitchButton _cannonSwitchButton;
	public UISwitchButton _laserSwitchButton;
	public UISwitchButton _missiledSwitchButton;
	public UISwitchButton _grenadeSwitchButton;

	public UIFilledSprite _cannonReloadingVisualisation;
	public UIFilledSprite _laserReloadingVisualisation;
	public UIFilledSprite _missilesReloadingVisualisation;
	public UIFilledSprite _grenadeReloadingVisualisation;

	public UISwitchImageButton _autoFireSwitchButton;
	public UIGrid _weaponsGrid;

	float _hudWidth;

	ActorScript _player;

	void Start ()
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorScript>();

		if (_player != null)
		{
			if (_cannonSwitchButton != null)
			{
				_cannonSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Projectile);
				_cannonSwitchButton.gameObject.transform.parent.gameObject.SetActive(ExistWeapon(WeaponScript.WeaponClasses.Projectile));
			}
			
			if (_laserSwitchButton != null)
			{
				_laserSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Energy);
				_laserSwitchButton.gameObject.transform.parent.gameObject.SetActive(ExistWeapon(WeaponScript.WeaponClasses.Energy));
			}
			
			if (_missiledSwitchButton != null)
			{
				_missiledSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Missile);
				_missiledSwitchButton.gameObject.transform.parent.gameObject.SetActive(ExistWeapon(WeaponScript.WeaponClasses.Missile));
			}
			
			if (_grenadeSwitchButton != null)
			{
				_grenadeSwitchButton.SwitchState = IsWeaponEnabled(WeaponScript.WeaponClasses.Grenade);
				_grenadeSwitchButton.gameObject.transform.parent.gameObject.SetActive(ExistWeapon(WeaponScript.WeaponClasses.Grenade));
			}

			if (_weaponsGrid != null)
			{
				_weaponsGrid.repositionNow = true;
			}

			if (_autoFireSwitchButton != null)
			{
				_autoFireSwitchButton.SwitchState = _player.gameObject.GetComponent<PlayerFireControlScript>()._autoFire;
			}
		}
	}
	
	void Update ()
	{
		if (_steamBar != null)
		{
			_steamBar.sliderValue = _player.Energy / _player.MaxEnergy;
		}

		if (_healthBar != null)
		{
			_healthBar.sliderValue = _player.Health / _player.MaxHealth;
		}

		UpdateWeaponsReloadUI();
	}

	void OnGUI() {

		float rx = Screen.width / NativeWidth;
		float ry = Screen.height / NativeHeight;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		DynamicWidth = NativeWidth * (rx / ry);
		DynamicHeight = NativeHeight;

		//_hudWidth = (_player.Weapons.Count + _player.Skills.Count) * DynamicWidth * 0.07f;
		_hudWidth = 400;//hack set the fix width
		if (_hudWidth < DynamicWidth * 0.35f)
			_hudWidth = DynamicWidth * 0.35f;

		if (!GameManager.Instance.Running)
			DrawOverviewUI();
		else 
			DrawThirdPersonUI();
	}

	void DrawOverviewUI()
	{
		GUILayout.BeginArea(new Rect(DynamicWidth * 0.01f, DynamicHeight * 0.8f, _hudWidth, DynamicHeight * 0.2f));
		GUILayout.BeginVertical(InfoHUDStyle);
		//GUILayout.Label("HP: " + _player.Health + "/" + _player.MaxHealth, InfoHUDStyle);
		//GUILayout.Label("Steam: " + _player.Energy + "/" + _player.MaxEnergy, InfoHUDStyle);


		GUILayout.BeginHorizontal(ActionBarStyle);
		/*foreach(var weapon in _player.Weapons)
		{
			if (GUILayout.Button(
				weapon.WeaponName 
				+ (weapon.AutoFire ? "\nON" : "\nOFF") + "\n" 
				+ (weapon.RemainingReloadTime > 0 ? weapon.RemainingReloadTime.ToString("0.0") : "")
				, GUILayout.ExpandHeight(true)))
			{
				weapon.AutoFire = !weapon.AutoFire;
			}
		}*/

		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		GUILayout.EndArea();

	}

	void DrawThirdPersonUI()
	{
		/*
		GUILayout.BeginArea(new Rect(DynamicWidth * 0.4f, DynamicHeight * 0.9f, DynamicWidth * 0.2f, DynamicHeight * 0.05f));
		{
			GUILayout.BeginVertical();
			{
				if (GUILayout.Button ("Pause"))
				{
					GameManager.Instance.SwitchToOverviewMode();
				}
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
		*/
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

	public void AutoFireHandle(GameObject gameObject)
	{
		UISwitchImageButton uiSwitchImageButton = gameObject.GetComponent<UISwitchImageButton>();
		if (uiSwitchImageButton != null)
		{
			Debug.Log("AutoFireHandle:" + uiSwitchImageButton.SwitchState);
			_player.gameObject.GetComponent<PlayerFireControlScript>()._autoFire = uiSwitchImageButton.SwitchState;
		}
	}

	private WeaponScript GetWeaponType(WeaponScript.WeaponClasses weaponType)
	{
		if (_player == null)
		{
			return null;
		}
		
		foreach(WeaponScript weapon in _player.Weapons)
		{
			if (weapon.WeaponClass == weaponType)
			{
				return weapon;
			}
		}

		return null;
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

	private bool ExistWeapon(WeaponScript.WeaponClasses weaponType)
	{
		if (_player == null)
		{
			return false;
		}
		
		foreach(WeaponScript weapon in _player.Weapons)
		{
			if (weapon.WeaponClass == weaponType)
			{
				return true;
			}
		}
		
		return false;
	}

	private void UpdateWeaponsReloadUI()
	{
		UpdateWeaponReloadUI(_cannonReloadingVisualisation, WeaponScript.WeaponClasses.Projectile);
		UpdateWeaponReloadUI(_laserReloadingVisualisation, WeaponScript.WeaponClasses.Energy);
		UpdateWeaponReloadUI(_missilesReloadingVisualisation, WeaponScript.WeaponClasses.Missile);
		UpdateWeaponReloadUI(_grenadeReloadingVisualisation, WeaponScript.WeaponClasses.Grenade);
	}

	private void UpdateWeaponReloadUI(UIFilledSprite filledSprite, WeaponScript.WeaponClasses weaponType)
	{
		WeaponScript weapon = GetWeaponType(weaponType);

		if (filledSprite != null && weapon != null)
		{
			filledSprite.fillAmount = 1 - (weapon.RemainingReloadTime / weapon.ReloadSpeed);
		}
	}
}
