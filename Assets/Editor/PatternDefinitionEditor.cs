using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(PatternDefinition))]
public class PatternDefinitionEditor : Editor
{
	public enum PatternTypes {
		Circle,
		Arc,
		Custom
	}

 	PatternTypes _patternType;
    int _pointCount;
	float _degrees;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		_patternType = (PatternTypes)EditorGUILayout.EnumPopup("Pattern Type", _patternType);
		switch(_patternType)
		{
		case PatternTypes.Circle:
			_pointCount = EditorGUILayout.IntField("Point count", _pointCount);
			break;
		case PatternTypes.Arc:
			_pointCount = EditorGUILayout.IntField("Point count", _pointCount);
			_degrees = EditorGUILayout.FloatField("Degrees", _degrees);
			break;
		}

		if (GUILayout.Button ("Update"))
			UpdatePattern((PatternDefinition)target);
	}

	void UpdatePattern(PatternDefinition pattern)
	{
		var targets = new Vector3[_pointCount];
		float radIncrement ;
		float radCounter;
		switch(_patternType)
		{
		case PatternTypes.Circle:
			radIncrement = 2.0f * Mathf.PI / _pointCount;
			radCounter = 0.0f;
			for (int i = 0; i < targets.Length; ++i) {
				targets[i].Set(Mathf.Sin(radCounter), 0, Mathf.Cos(radCounter));
				radCounter += radIncrement;
			}
			break;
		case PatternTypes.Arc:
			radIncrement = Mathf.Deg2Rad * _degrees;
			radCounter = (_pointCount - 1)* radIncrement / 2.0f;
			for (int i = 0; i < targets.Length; ++i) {
				targets[i].Set(Mathf.Sin(radCounter), 0, Mathf.Cos(radCounter));
				radCounter -= radIncrement;
			}
			break;

		}

		pattern.FireTargets = targets;
	}
}
