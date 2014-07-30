using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorScript : MonoBehaviour {

	/*
	public enum ActorBehaviourModes 
	{
		Static,
		Aggresive,
		Defensive
	}

	public enum ActorTeams
	{
		Red,
		Blue
	}

	public ActorTeams ActorTeam;

	public GameObject ActorBase;
	public GameObject ActorTurret;

	public WaypointPlanner Waypoints;

	public float SensorRange;
	public float ReactionRange;

	public ActorBehaviourModes BehaviorMode;

	SphereCollider _sensors;

	List<GameObject> _targets;



	void Start () {
		_sensors = ActorType.AddComponent<SphereCollider>();
		_sensors.radius = SensorRange;
		_sensors.isTrigger = true;

		_targets = new List<GameObject>();
	}
	
	void Update () {
		if (_targets.Any())
		{
			Track(_targets[0]);
			if (IsFireSolution(_targets[0]))
				KeepFiring();
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		var actor = other.GetComponentInChildren<ActorScript>(other.gameObject);
		if (actor != null && actor.ActorTeam != ActorTeam)
			AddTarget(other.gameObject);
	}

	void OnTriggerLeave(Collider other)
	{
		var actor = other.GetComponentInChildren<ActorScript>(other.gameObject);
		if (actor != null && actor.ActorTeam != ActorTeam)
			RemoveTarget(other.gameObject);
	}


	void AddTarget(GameObject target)
	{
		_targets.Add(target);
	}

	void RemoveTarget(GameObject target)
	{
		_targets.Remove(target);
	}

	void Track(GameObject target)
	{
		var targetVector = target.transform.position - transform.position;
		 
	}

	bool HasFireSolution(GameObject target)
	{
	}

	void KeepFiring(GameObject target)
	{
	}
	*/

}
