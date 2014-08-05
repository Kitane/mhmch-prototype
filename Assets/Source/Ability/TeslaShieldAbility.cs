using UnityEngine;
using System.Collections;

public class TeslaShieldAbility : IAbility
{

	/*public TeslaShieldAbility(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) : base(attributes, sourcePos, owner) {}
	
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
		
		if (_attributes.Projectile.Model != null)
		{
			_attributes.Projectile.Model.SetActive(true);//show tesla shield object
		}
	}
	
	public override void Update()
	{
		if (_timeElapsed < _attributes.Projectile.Duration)
		{
			_timeElapsed += Time.deltaTime;//tick
		}
		else
		{
			if (_ownerActor._mechAnimator != null && !string.IsNullOrEmpty(_ownerWeapon.FiringAnimName))
			{
				_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, false);
			}
			
			_attributes.Projectile.Model.SetActive(false);//hide tesal shield
			_firing = false;
		}
	}*/
}
