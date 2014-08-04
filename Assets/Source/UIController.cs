using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public float NativeWidth = 720;
	public float NativeHeight = 1280;

	public float DynamicWidth;
	public float DynamicHeight;

	public GUIStyle InfoHUDStyle;
	public GUIStyle ActionBarStyle;

	float _hudWidth;

	ActorScript _player;

	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorScript>();

	}
	
	void Update () {
	
	}

	void OnGUI() {

		float rx = Screen.width / NativeWidth;
		float ry = Screen.height / NativeHeight;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		DynamicWidth = NativeWidth * (rx / ry);
		DynamicHeight = NativeHeight;

		//_hudWidth = (_player.Weapons.Count + _player.Skills.Count) * DynamicWidth * 0.07f;
		_hudWidth = 250;//hack set the fix width
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
		GUILayout.Label("HP: " + _player.Health + "/" + _player.MaxHealth, InfoHUDStyle);
		GUILayout.Label("Steam: " + _player.Energy + "/" + _player.MaxEnergy, InfoHUDStyle);


		GUILayout.BeginHorizontal(ActionBarStyle);
		foreach(var weapon in _player.Weapons)
		{
			if (GUILayout.Button(
				weapon.WeaponName 
				+ (weapon.AutoFire ? "\nON" : "\nOFF") + "\n" 
				+ (weapon.RemainingReloadTime > 0 ? weapon.RemainingReloadTime.ToString("0.0") : "")
				, GUILayout.ExpandHeight(true)))
				weapon.AutoFire = !weapon.AutoFire;
		}
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
}
