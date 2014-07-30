using UnityEngine;
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

	public WaypointPlanner Waypoints;

	//actor parts to control
	public GameObject 	Legs;
	public GameObject 	Torso;

	public NavMeshAgent _navAgent;

	public void SetDestination(Vector3 destination)
	{
		_navAgent.SetDestination(destination);
	}

	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
	{
		if (CurrentTarget != null)
			Track(CurrentTarget);
		else
			StopTracking();
	}

	void Track(GameObject target)
	{
		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		Torso.transform.rotation = Quaternion.RotateTowards(Torso.transform.rotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);
	}

	void StopTracking()
	{
		if (Torso != null)
			Torso.transform.localRotation = Quaternion.RotateTowards(Torso.transform.localRotation, Quaternion.identity, TorsoTwistSpeedDeg * Time.deltaTime);
	}
}
