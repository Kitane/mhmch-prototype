﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ActorScript : MonoBehaviour {

	// prefab
	public GameObject 	ActorType;
	// team allegiance
	public int 			ActorTeam;

	public float 		TorsoTwistSpeedDeg = 60;
	public int 			Health;
	public int 			MaxHealth;

	public GameObject 	CurrentTarget;
	public Vector3 		CurrentDestination;

	public WaypointPlanner Waypoints;

	//actor parts to control
	public GameObject 	Legs;
	public GameObject 	Torso;


	void Start () {

	}
	
	void Update () 
	{
		if (CurrentTarget != null)
			Track(CurrentTarget);
	}

	void Track(GameObject target)
	{
		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		Torso.transform.rotation = Quaternion.RotateTowards(Torso.transform.rotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);
	}
}
