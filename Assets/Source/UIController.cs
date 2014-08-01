using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public float NativeWidth = 1280;
	public float NativeHeight = 720;

	public float DynamicWidth;
	public float DynamicHeight;

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

		if (!GameManager.Instance.Running)
			DrawOverviewUI();
		else 
			DrawThirdPersonUI();
	}

	void DrawOverviewUI()
	{
		GUILayout.BeginArea(new Rect(DynamicWidth * 0.05f, DynamicWidth * 0.2f, DynamicHeight * 0.1f, DynamicWidth * 0.05f));
		GUILayout.BeginVertical();
		GUILayout.Label("HP: " + _player.Health + "/" + _player.MaxHealth);
		GUILayout.Label("Steam Pressure: " + _player.Energy + "/" + _player.MaxEnergy);
		GUILayout.EndVertical();
		GUILayout.EndArea();


		GUILayout.BeginArea(new Rect(DynamicWidth * 0.3f, DynamicWidth * 0.2f, DynamicHeight * 0.1f, DynamicWidth * 0.05f));
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
