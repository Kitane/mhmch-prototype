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

	List<Transform> _targets;
	ActorScript _actor;

	float _optimumRange;

	void Update()
	{

		if (_actor.CurrentTarget != null && BehaviorMode != BehaviourModes.Static) {
			if (_actor.CurrentTarget.GetComponentInParent<ActorScript>().Dead)
				RemoveTarget(_actor.CurrentTarget);
			else {
				float destinationToTarget = (transform.position - _actor.CurrentTarget.transform.position).magnitude;
				if (destinationToTarget > _optimumRange) {
					_actor.SetDestination(_actor.CurrentTarget.transform.position);
				} else {
					_actor.Stop();
				}

				foreach(var weapon in _actor.Weapons)
				{
					if (_actor.TorsoAlignedToTarget && weapon.Ready && weapon.Range > destinationToTarget && weapon.Cost <= _actor.Energy)
						weapon.Fire(_actor.CurrentTarget);
				}
			}
		}
	}

	void Start()
	{
		_actor = GetComponentInParent<ActorScript>();

		_optimumRange = _actor.GetComponentsInChildren<WeaponScript>().Min(x => x.Range) * 0.8f;

		_targets = new List<Transform>();
	}

	void OnTriggerEnter(Collider other) 
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
			AddTarget(other.gameObject.transform);
	}
	
	void OnTriggerExit(Collider other)
	{
		var actor = other.GetComponentInParent<ActorScript>();
		if (actor != null && actor.ActorTeam != _actor.ActorTeam)
			RemoveTarget(other.gameObject.transform);
	}

	void AddTarget(Transform target)
	{
		if (!target.parent.gameObject.GetComponent<ActorScript>().Dead) {
			_targets.Add(target);
			_actor.CurrentTarget = target.parent.Find("Hitzone");
		}
	}
	
	void RemoveTarget(Transform target)
	{
		_targets.Remove(target);
		if (_targets.Any())
			_actor.CurrentTarget =_targets[0].parent.Find ("Hitzone");
		else
			_actor.CurrentTarget = null;
	}


}
