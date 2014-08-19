using UnityEngine;
using System.Collections;
using System.Collections.Generic;



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


