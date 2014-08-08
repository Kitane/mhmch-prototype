using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BurstAttributes
{
	public int Rounds;
	public float RateOfFire;
	public ProjectileAttributes Projectile;
}

public abstract class BurstGenerator
{
	protected BurstAttributes _attributes;
	protected GameObject _sourcePos;
	protected WeaponScript _ownerWeapon;
	protected ActorScript _ownerActor;

	public BurstGenerator(BurstAttributes attributes, GameObject sourcePos, WeaponScript owner) 
	{ 
		_attributes = attributes; 
		_sourcePos = sourcePos; 
		_ownerWeapon = owner;
		_ownerActor = owner.GetComponentInParent<ActorScript>();
	}

	public abstract void Fire(Transform target);
	public abstract void Update();
}

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
		if (_roundsRemaining > 0 && _timeElapsed <= 0.0f && _target != null)
		{
			//TODO FIRE bullet 
			var rotation = Quaternion.LookRotation(_target.position - _sourcePos.transform.position );

			GameObject bullet = (GameObject)GameObject.Instantiate(_attributes.Projectile.Model, _sourcePos.transform.position, rotation);
			var bulletScript = bullet.GetComponent<ProjectileScript>();
			bulletScript.RemainingTime = _attributes.Projectile.Duration;
			bulletScript.Definition = _attributes.Projectile;
			bulletScript.Team = _ownerActor.ActorTeam;

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
}

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

public class WeaponScript : MonoBehaviour 
{
	public enum WeaponClasses
	{
		Energy,
		Missile,
		Projectile,
		Grenade
	}

	public float Range { 
		get { 
			return Burst.Projectile.Speed * Burst.Projectile.Duration; 
		}
	}

			
	public string WeaponName;
	public WeaponClasses WeaponClass;
	public float ReloadSpeed;
	public float Cost;
	public string FiringAnimName;

	public BurstAttributes Burst;
	
	public bool enabled;

	public GameObject Model;

	public float RemainingReloadTime;
	public bool Ready { get; private set; }

	BurstGenerator _generator;

	ActorScript _actor;

	public void Fire(Transform target)
	{
		RemainingReloadTime = ReloadSpeed;
		Ready = false;
		_generator.Fire(target);
		_actor.PayCost(Cost);
	}

	void Start()
	{
		switch(WeaponClass)
		{
		case WeaponClasses.Energy:
			_generator = new BeamGenerator(Burst, Model, this);
			break;
			
		case WeaponClasses.Missile:
			_generator = new MissileGenerator(Burst, Model, this);
			break;
			
		case WeaponClasses.Projectile:
			_generator = new ProjectileGenerator(Burst, Model, this);
			break;
		case WeaponClasses.Grenade:
			_generator = new GrenadeGenerator(Burst, Model, this);
			break;
		}
		_actor = GetComponentInParent<ActorScript>();
	}

	void Update()
	{
		if (!Ready) {
			RemainingReloadTime -= Time.deltaTime;
			Ready = RemainingReloadTime < 0;
		}

		if(_generator != null)
			_generator.Update();
	}
}


