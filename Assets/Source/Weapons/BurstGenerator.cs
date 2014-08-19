using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
