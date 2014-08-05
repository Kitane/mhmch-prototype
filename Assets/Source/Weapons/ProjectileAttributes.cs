using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProjectileAttributes
{
	public WeaponScript.WeaponClasses WeaponClass;
	public float Damage;
	public float Speed;
	public float Duration;

	public float TrackingSpeed;
	
	public GameObject Model;
}
