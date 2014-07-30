using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ActorScript : MonoBehaviour {

	public GameObject ActorType;

	public int ActorTeam;
	
	public GameObject Legs;
	public GameObject Torso;

	public float TorsoTwistSpeedDeg = 60;
	public float TorsoTwistRange;

	public GameObject Target;

	public WaypointPlanner Waypoints;
	
	void Start () {

	}
	
	void Update () 
	{
		if (Target != null)
			Track(Target);
	}

	void Track(GameObject target)
	{
		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		Torso.transform.rotation = Quaternion.RotateTowards(Torso.transform.rotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);
	}
}
