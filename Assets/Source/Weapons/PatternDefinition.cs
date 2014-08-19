using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PatternDefinition : MonoBehaviour
{
	public Vector3[] FireTargets;
	public Vector3[] SpawnPoints;

	List<Transform> _fireTargets = new List<Transform>();
	List<Transform> _spawnPoints = new List<Transform>();

	int _targetIndex;
	int _spawnIndex;

	void Start()
	{
		foreach (var point in FireTargets)
		{
			var t = new GameObject();
			t.transform.parent = gameObject.transform;
			t.transform.localPosition = point;
			_fireTargets.Add(t.transform);
		}

		foreach (var point in SpawnPoints)
		{
			var t = new GameObject();
			t.transform.parent = gameObject.transform;
			t.transform.localPosition = point;
			_spawnPoints.Add(t.transform);
		}
	}

	public bool AlternateSpawnPoints()
	{
		return SpawnPoints.Length > 0;
	}

	public Vector3 GetNextSpawnPoint()
	{
		return GetNextPoint(_spawnPoints, ref _targetIndex);
	}

	public Vector3 GetNextTargetPoint()
	{
		return GetNextPoint(_fireTargets, ref _spawnIndex);
	}

	Vector3 GetNextPoint(List<Transform> points, ref int index)
	{
		var result = points[index++].position;
		if (index == points.Count)
			index = 0;
		return result;
	}
}
