using UnityEditor;
using System.Collections;
using UnityEngine;

            

[CustomEditor(typeof(BuildingGenerator))]
public class BuildingGeneratorEditor : Editor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		BuildingGenerator target_ = (BuildingGenerator)target;

		if ( GUILayout.Button("Generate") )
		{
			target_.Generate();
		}

		if ( GUILayout.Button ("Clear") )
		{
			target_.Clear();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
