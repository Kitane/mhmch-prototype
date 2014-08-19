using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileGenerator : BurstGenerator
{
	public ProjectileGenerator(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) : base(attributes, sourcePos, owner) {}

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
		if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
		else if (_firing) {
			if (_ownerActor._mechAnimator != null)
				_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, false);
			_firing = false;
		}
		else 
			while (_roundsRemaining > 0 && _timeElapsed <= 0.0f && _target != null)
			{
				Vector3 projectileTarget;
				Vector3 projectileSource;
				if ((_attributes.Pattern != null)) 
				{
					projectileTarget = _attributes.Pattern.GetNextTargetPoint();
					projectileSource = _attributes.Pattern.AlternateSpawnPoints() ? _attributes.Pattern.GetNextSpawnPoint() : _attributes.Pattern.transform.position;
				} 
				else
				{
					projectileTarget = _target.position;
					projectileSource = _sourcePos.transform.position;
				}

			//tmpfix
				var direction = projectileTarget - projectileSource;
				direction.y = 0.0f;
				var rotation = Quaternion.LookRotation(direction);

				GameObject bullet = (GameObject)GameObject.Instantiate(_attributes.Projectile.Model, projectileSource, rotation);
				var bulletScript = bullet.GetComponent<ProjectileScript>();
				bulletScript.RemainingTime = _attributes.Projectile.Duration;
				bulletScript.Definition = _attributes.Projectile;
				bulletScript.Team = _ownerActor.ActorTeam;

				_timeElapsed = _attributes.RateOfFire;
				_roundsRemaining--;
			}
	}
}
