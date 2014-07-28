using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingGenerator : MonoBehaviour {

	public Transform ObstacleContainer;

	public int StartingOffset = 10;
	public int BuildingDimension = 32;
	public int BuildingOffset = 40;
	public int BuildingCount = 100;


	public Transform Building;

	System.Random _random = new System.Random(1);

	List<Transform> _buildings = new List<Transform>();

	// Use this for initialization
	public void Generate () 
	{
		var buildingMatrix = new bool[BuildingDimension * BuildingDimension];

		for (int i = 0; i < BuildingCount; i++)
		{
			int x;
			int y;
			GenerateCoords(out x, out y);
			while (buildingMatrix[y * BuildingDimension + x]) {
				GenerateCoords(out x, out y);
			}

			buildingMatrix[y * BuildingDimension + x] = true;

			Transform obstacle = (Transform)Instantiate(Building, new Vector3(x + StartingOffset, 0, y + StartingOffset) * BuildingOffset, Quaternion.identity);
			obstacle.parent = ObstacleContainer;
			_buildings.Add(obstacle);
		}
	}

	public void Clear () 
	{
		foreach(var building in _buildings)
		{
			Object.DestroyImmediate(building.gameObject);
		}
		_buildings.Clear();
	}

	void GenerateCoords(out int x, out int y)
	{
		x = _random.Next(BuildingDimension);
	 y = _random.Next(BuildingDimension);
	}

}
