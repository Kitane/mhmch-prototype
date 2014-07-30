using UnityEngine;
using System.Collections.Generic;

public class AIScript : MonoBehaviour 
{
	public enum BehaviourModes 
	{
		Static,
		Aggresive,
		Defensive
	}

	public BehaviourModes BehaviorMode;
	public float ReactionRange;


	SphereCollider _sensors;
	List<GameObject> _targets;
	ActorScript _actor;

	void Update()
	{

	}

	void OnStart()
	{
		_actor = GetComponentInParent<ActorScript>();

		_sensors = _actor.ActorType.AddComponent<SphereCollider>();
		_sensors.radius = ReactionRange;
		_sensors.isTrigger = true;
		
		_targets = new List<GameObject>();
	}

	void OnTriggerEnter(Collider other) 
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
			AddTarget(other.gameObject);
	}
	
	void OnTriggerLeave(Collider other)
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
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
}
