using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponScript : MonoBehaviour 
{
	public enum WeaponClasses
	{
		Energy,
		Missile,
		Projectile
	}

	public string Name;
	public WeaponClasses WeaponClass;
	public float Range;
	public float ReloadSpeed;
	public ProjectileAttributes Generator;

	public GameObject Model;

	public void FireSalvo(GameObject Target)
	{


	}

}


