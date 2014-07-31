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

	public BurstGenerator(BurstAttributes attributes, GameObject sourcePos) { _attributes = attributes; _sourcePos = sourcePos; }
	public abstract void Fire(GameObject target);
	public abstract void Update();
}

public class BeamGenerator : BurstGenerator
{
	public BeamGenerator(BurstAttributes attributes, GameObject sourcePos) : base(attributes, sourcePos) 
	{
		_renderer = sourcePos.AddComponent<LineRenderer>();
		_renderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		_renderer.SetColors(Color.blue, Color.blue);
		_renderer.SetWidth(0.2f, 0.2f);
		_renderer.SetVertexCount(2);
	}

	float _timeElapsed;
	int _pulsesRemaining;
	GameObject _target;
	bool _beamOn;

	LineRenderer _renderer;

	public override void Fire(GameObject target)
	{
		_timeElapsed = 0.0f;
		_pulsesRemaining = _attributes.Rounds;
		_target = target;
		_beamOn = false;
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

				if (_pulsesRemaining > 0)
					_timeElapsed = _attributes.RateOfFire;
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
			_renderer.SetPosition(1, _target.transform.position);
		}
	}
}

public class MissileGenerator : BurstGenerator
{
	public MissileGenerator(BurstAttributes attributes, GameObject sourcePos) : base(attributes, sourcePos) {}

	float _timeElapsed;
	int _roundsRemaining;
	GameObject _target;

	public override void Fire(GameObject target)
	{
		_timeElapsed = 0.0f;
		_roundsRemaining = _attributes.Rounds;
		_target = target;
	}
	
	public override void Update()
	{
		if (_roundsRemaining > 0 && _timeElapsed <= 0.0f)
		{
			//TODO FIRE bullet 
			var rotation = Quaternion.LookRotation(_target.transform.position - _sourcePos.transform.position );
			
			GameObject bullet = (GameObject)GameObject.Instantiate(_attributes.Projectile.Model, _sourcePos.transform.position, rotation);
			var bulletScript = bullet.GetComponent<ProjectileScript>();
			bulletScript.RemainingTime = _attributes.Projectile.Duration;
			bulletScript.Definition = _attributes.Projectile;
			bulletScript.Target = _target;
			
			_timeElapsed = _attributes.RateOfFire;
			_roundsRemaining--;
		} else if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
	}
}

public class ProjectileGenerator : BurstGenerator
{
	public ProjectileGenerator(BurstAttributes attributes, GameObject sourcePos) : base(attributes, sourcePos) {}

	float _timeElapsed;
	int _roundsRemaining;
	GameObject _target;
	
	public override void Fire(GameObject target)
	{
		_timeElapsed = 0.0f;
		_roundsRemaining = _attributes.Rounds;
		_target = target;
	}
	
	public override void Update()
	{
		if (_roundsRemaining > 0 && _timeElapsed <= 0.0f)
		{
			//TODO FIRE bullet 
			var rotation = Quaternion.LookRotation(_target.transform.position - _sourcePos.transform.position );

			GameObject bullet = (GameObject)GameObject.Instantiate(_attributes.Projectile.Model, _sourcePos.transform.position, rotation);
			var bulletScript = bullet.GetComponent<ProjectileScript>();
			bulletScript.RemainingTime = _attributes.Projectile.Duration;
			bulletScript.Definition = _attributes.Projectile;

			_timeElapsed = _attributes.RateOfFire;
			_roundsRemaining--;
		} else if (_timeElapsed > 0.0f)
			_timeElapsed -= Time.deltaTime;
	}
}

public class WeaponScript : MonoBehaviour 
{
	public enum WeaponClasses
	{
		Energy,
		Missile,
		Projectile
	}

	public float Range { 
		get { 
			return Burst.Projectile.Speed * Burst.Projectile.Duration; 
		}
	}

			
	public string Name;
	public WeaponClasses WeaponClass;
	public float ReloadSpeed;
	public float Cost;

	public BurstAttributes Burst;

	public bool AutoFire;

	public GameObject Model;

	public float RemainingReloadTime;
	public bool Ready { get; private set; }

	BurstGenerator _generator;

	public void Fire(GameObject target)
	{
		RemainingReloadTime = ReloadSpeed;
		Ready = false;
		_generator.Fire(target);
	}

	void Start()
	{
		switch(WeaponClass)
		{
		case WeaponClasses.Energy:
			_generator = new BeamGenerator(Burst, Model);
			break;
			
		case WeaponClasses.Missile:
			_generator = new MissileGenerator(Burst, Model);
			break;
			
		case WeaponClasses.Projectile:
			_generator = new ProjectileGenerator(Burst, Model);
			break;
		}
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

