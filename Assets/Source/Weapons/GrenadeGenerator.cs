using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeGenerator : BurstGenerator
{
	public GrenadeGenerator(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) : base(attributes, sourcePos, owner) {}
	
	float _timeElapsed;
	int _roundsRemaining;
	Transform _target;
	bool _firing;
	
	public override void Fire(Transform target)
	{
		_timeElapsed = 0.0f;
		_roundsRemaining = _attributes.Rounds;
		_target = target;
		_firing = true;
	}
	
	public override void Update()
	{
		if (_roundsRemaining > 0 && _timeElapsed <= 0.0f)
		{
			//TODO FIRE bullet 
			var rotation = Quaternion.LookRotation(_target.position - _sourcePos.transform.position );
			
			GameObject bullet = (GameObject)GameObject.Instantiate(_attributes.Projectile.Model, _sourcePos.transform.position, rotation);
			GrenadeScript grenadeScript = bullet.GetComponent<GrenadeScript>();
			grenadeScript.RemainingTime = _attributes.Projectile.Duration;
			grenadeScript.Definition = _attributes.Projectile;
			grenadeScript.Team = _ownerActor.ActorTeam;
			
			//prepare speed to throw
			Vector3 throwSpeed = calculateBestThrowSpeed(_sourcePos.transform.position, _target.transform.position, 3);

			Debug.Log ("Grenade fire:" + throwSpeed.ToString());
			
			//search for rigid body
			Rigidbody grenadeRigidBody = grenadeScript._grenadeRigidBody;
			grenadeRigidBody.AddForce(throwSpeed, ForceMode.VelocityChange);
			
			_timeElapsed = _attributes.RateOfFire;
			_roundsRemaining--;
		} else if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
		else if (_firing) {
			if (_ownerActor._mechAnimator != null)
				_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, false);
			_firing = false;
		}
	}
	
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;
		
		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;
		
		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;
		
		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)
		
		return result;
	}
}
