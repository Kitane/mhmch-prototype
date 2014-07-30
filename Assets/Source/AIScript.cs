using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

	List<GameObject> _targets;
	ActorScript _actor;

	void Update()
	{

	}

	void Start()
	{
		_actor = GetComponentInParent<ActorScript>();

		_targets = new List<GameObject>();
	}

	void OnTriggerEnter(Collider other) 
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
			AddTarget(other.gameObject);
	}
	
	void OnTriggerExit(Collider other)
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
			RemoveTarget(other.gameObject);
	}
	
	void AddTarget(GameObject target)
	{
		_targets.Add(target);
		_actor.CurrentTarget = target;
	}
	
	void RemoveTarget(GameObject target)
	{
		_targets.Remove(target);
		if (_targets.Any())
			_actor.CurrentTarget =_targets[0];
		else
			_actor.CurrentTarget = null;
	}
}
