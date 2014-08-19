using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeamGenerator : BurstGenerator
{
	public BeamGenerator(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) : base(attributes, sourcePos, owner) 
	{
		_renderer = sourcePos.AddComponent<LineRenderer>();
		_renderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		_renderer.SetColors(Color.blue, Color.blue);
		_renderer.SetWidth(0.2f, 0.2f);
		_renderer.SetVertexCount(2);
	}

	float _timeElapsed;
	int _pulsesRemaining;
	Transform _target;
	bool _beamOn;

	LineRenderer _renderer;

	public override void Fire(Transform target)
	{
		_timeElapsed = 0.0f;
		_pulsesRemaining = _attributes.Rounds;
		_target = target;
		_beamOn = false;

		if (_ownerActor._mechAnimator != null)
			_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, true);

	}

	public override void Update()
	{
		if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
	    else 
		{
			if (_beamOn) 
			{
				_beamOn = false;
				_renderer.enabled = false;
				_target.gameObject.GetComponentInParent<ActorScript>().ReceiveDamage(_attributes.Projectile.Damage);

				if (_pulsesRemaining > 0)
					_timeElapsed = _attributes.RateOfFire;
				else
					if (_ownerActor._mechAnimator != null)
						_ownerActor._mechAnimator.SetBool(_ownerWeapon.FiringAnimName, false);
			} 
			else 
			{
				if (_pulsesRemaining > 0)
				{
					_timeElapsed = _attributes.RateOfFire;
					_pulsesRemaining--;
					_beamOn = true;
					_renderer.enabled = true;
				}
			}
		}

		if (_beamOn)
		{
			_renderer.SetPosition(0, _sourcePos.transform.position);
			_renderer.SetPosition(1, _target.position);
		}
	}
}
