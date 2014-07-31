using UnityEngine;
using System.Linq;
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
	public bool 		TorsoAlignedToTarget { get; private set;}

	public WaypointPlanner Waypoints;

	//actor parts to control
	public GameObject 	Legs;
	public GameObject 	Torso;

	public List<WeaponScript> Weapons;

	public NavMeshAgent _navAgent;
	public Animator _mechAnimator;

	public void SetDestination(Vector3 destination)
	{
		_navAgent.SetDestination(destination);
		if (_mechAnimator != null)
		{
			_mechAnimator.SetBool("walk", true);
			_mechAnimator.SetBool("stop", false);
		}
	}

	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();

		Weapons = new List<WeaponScript> (GetComponentsInChildren<WeaponScript>());
	}
	
	void Update () 
	{
		if (CurrentTarget != null)
			Track(CurrentTarget);
		else
			StopTracking();

		//is walking
		if (_mechAnimator != null && _mechAnimator.GetBool("walk"))
		{
			float dist = _navAgent.remainingDistance;

			// Check if we've reached the destination
			if (!_navAgent.pathPending)
			{
				if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
				{
					if (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)
					{
						// Done
						Debug.Log ("on the place");
						_mechAnimator.SetBool("walk", false);
						_mechAnimator.SetBool("stop", true);
					}
				}
			}
		}
	}

	void Track(GameObject target)
	{
		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);


		float angle = Quaternion.Angle(targetRotation, Torso.transform.rotation);
		TorsoAlignedToTarget = angle < 2.0f;

		Torso.transform.rotation = Quaternion.RotateTowards(Torso.transform.rotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);
	}

	void StopTracking()
	{
		if (Torso != null)
			Torso.transform.localRotation = Quaternion.RotateTowards(Torso.transform.localRotation, Quaternion.identity, TorsoTwistSpeedDeg * Time.deltaTime);
	}
}
