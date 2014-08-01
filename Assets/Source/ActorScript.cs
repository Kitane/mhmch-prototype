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
	public float 		Health;
	public float 		MaxHealth;
	public float 		DamageShakeTreshold = 5.0f;

	public float		Energy;
	public float		MaxEnergy;
	public float		EnergyRegenRate;

	public Transform 	CurrentTarget;
	public bool 		TorsoAlignedToTarget { get; private set;}

	public WaypointPlanner Waypoints;

	//actor parts to control
	public GameObject 	Legs;
	public GameObject 	Torso;


	public List<WeaponScript> Weapons;

	public NavMeshAgent _navAgent;
	public Animator _mechAnimator;

	public Vector3 _prevPosition;

	public bool Dead { get { return Health <= 0; } }


	public void SetDestination(Vector3 destination)
	{
		if (Dead)
			return;

		_navAgent.SetDestination(destination);
		if (_mechAnimator != null)
		{
			_mechAnimator.SetBool("walk", true);
			_mechAnimator.SetBool("stop", false);
		}
	}

	public void Stop()
	{
		_navAgent.Stop();
		if (_mechAnimator != null)
		{
			_mechAnimator.SetBool("walk", false);
			_mechAnimator.SetBool("stop", true);
		}
	}

	public void ReceiveDamage(float damage)
	{
		Health -= damage;
		if (damage >= DamageShakeTreshold)
		{
			StartCoroutine(PlayOneShot("hit"));
		}
		if (Health <= 0) {
			_navAgent.Stop(true);
			StartCoroutine(PlayOneShot("death"));
		}
	}

	public void PayCost(float cost)
	{
		Energy -= cost;
	}

	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();

		Weapons = new List<WeaponScript> (GetComponentsInChildren<WeaponScript>());
	}
	
	void Update () 
	{
		if (Dead)
			return;

		if (CurrentTarget != null)
			Track(CurrentTarget);
		else
			StopTracking();

		if (Energy < MaxEnergy)
		{
			Energy += EnergyRegenRate * Time.deltaTime;
			if (Energy > MaxEnergy)
				Energy = MaxEnergy;
		}


		//is walking
		if (_mechAnimator != null && _mechAnimator.GetBool("walk"))
		{
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

	void Track(Transform target)
	{
		var targetRotation = Quaternion.LookRotation(target.position - transform.position);


		float angle = Quaternion.Angle(targetRotation, Torso.transform.rotation);
		TorsoAlignedToTarget = angle < 2.0f;

		Torso.transform.rotation = Quaternion.RotateTowards(Torso.transform.rotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);
	}

	void StopTracking()
	{
		if (Torso != null)
			Torso.transform.localRotation = Quaternion.RotateTowards(Torso.transform.localRotation, Quaternion.identity, TorsoTwistSpeedDeg * Time.deltaTime);
	}

	public IEnumerator PlayOneShot(string paramName)
	{
		_mechAnimator.SetBool(paramName, true);
		yield return null;
		_mechAnimator.SetBool(paramName, false);
	}

}
