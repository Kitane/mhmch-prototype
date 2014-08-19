using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BurstAttributes
{
	public int Rounds;
	public float RateOfFire;
	public ProjectileAttributes Projectile;

	public string PatternName;
	//[HideInInspector]
	public PatternDefinition Pattern;
}
