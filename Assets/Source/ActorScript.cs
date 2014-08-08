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
	Quaternion			_previousTorsoRotation;
	public Vector3 		_rotationHack;

	public WaypointPlanner Waypoints;

	//actor parts to control
	public GameObject 	Legs;
	public GameObject 	Torso;


	public List<WeaponScript> Weapons;

	public List<SkillScript> Skills;

	public NavMeshAgent _navAgent;
	public Animator _mechAnimator;

	public Vector3 _prevPosition;

	public bool Dead { get { return Health <= 0; } }

	private int _timeToGameOver = 200;
	private string[] ANIMATION_STATES = {"walk", "stop", "shootL", "shootR", "rocket", "hit", "death", "run"};


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
			if (_mechAnimator != null && Health > 0)
			{
				StartCoroutine(PlayOneShot("hit"));
			}
		}

		if (_mechAnimator != null)
		{
			_mechAnimator.SetFloat("health", Health);
		}

		if (Health <= 0)
		{
			if (_navAgent != null)
			{
				_navAgent.Stop(true);
			}

			if (_mechAnimator != null && !_mechAnimator.GetBool("death"))
			{
				Debug.Log ("health:" + _mechAnimator.GetFloat("health"));
				SetAnimationStateWithStopOthers("death");
			}
		}

	}

	public void PayCost(float cost)
	{
		Energy -= cost;
	}

	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();

		Weapons = new List<WeaponScript> (GetComponentsInChildren<WeaponScript>());
		Skills = new List<SkillScript> (GetComponentsInChildren<SkillScript>());
		_previousTorsoRotation = Quaternion.identity;
	}

	void LateUpdate()
	{
		if (Dead)
			return;

		if (CurrentTarget != null)
			Track(CurrentTarget);
		else {
			StopTracking();
		}
	}
	
	void Update () 
	{
		if (Dead)
		{
			if (_timeToGameOver-- < 0 && ActorTeam == 1)
			{
				Application.LoadLevel("GameOverScene");//show game over
			}

			return;
		}

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

		float angle = Quaternion.Angle(targetRotation, _previousTorsoRotation);
		TorsoAlignedToTarget = angle < 2.0f;

		_previousTorsoRotation = Quaternion.RotateTowards(_previousTorsoRotation, targetRotation, TorsoTwistSpeedDeg * Time.deltaTime);

		if (_mechAnimator == null)
			Torso.transform.rotation = _previousTorsoRotation;
		else {
			Torso.transform.rotation = _previousTorsoRotation * Quaternion.Euler(_rotationHack);
		}
	}

	void StopTracking()
	{
		if (Torso != null) {

			_previousTorsoRotation = Quaternion.RotateTowards(_previousTorsoRotation, transform.rotation, TorsoTwistSpeedDeg * Time.deltaTime);
			if (_mechAnimator == null)
				Torso.transform.rotation =_previousTorsoRotation;
			else {
				Torso.transform.rotation = _previousTorsoRotation * Quaternion.Euler(_rotationHack);;
			}
		}
	}


	public IEnumerator PlayOneShot(string paramName)
	{
		_mechAnimator.SetBool(paramName, true);
		yield return null;
		_mechAnimator.SetBool(paramName, false);
	}

	private void SetAnimationStateWithStopOthers(string stateName)
	{
		foreach (string stateNameToSet in ANIMATION_STATES)
		{
			_mechAnimator.SetBool(stateNameToSet, stateName == stateNameToSet);//set true for our state and false for all others
		}
	}
}
