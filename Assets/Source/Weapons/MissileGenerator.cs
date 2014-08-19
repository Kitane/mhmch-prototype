using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileGenerator : BurstGenerator
{
	public MissileGenerator(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) : base(attributes, sourcePos, owner) {}

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
			var bulletScript = bullet.GetComponent<ProjectileScript>();
			bulletScript.RemainingTime = _attributes.Projectile.Duration;
			bulletScript.Definition = _attributes.Projectile;
			bulletScript.Target = _target;
			bulletScript.Team = _ownerActor.ActorTeam;
			
			_timeElapsed = _attributes.RateOfFire;
			_roundsRemaining--;

		} else if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
		else if (_firing) {
			if (_ownerActor._mechAnimator != null && !string.IsNullOrEmpty(_ownerWeapon.FiringAnimName))
				_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, false);
			_firing = false;
		}
	}
}
