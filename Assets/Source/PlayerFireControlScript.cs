﻿using UnityEngine;
using System.Collections;

public class PlayerFireControlScript : MonoBehaviour {

	ActorScript _actor;
	// Use this for initialization

	void Start () {
		_actor = GetComponent<ActorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_actor.CurrentTarget != null) {
			float destinationToTarget = (transform.position - _actor.CurrentTarget.transform.position).magnitude;

			if (_actor.CurrentTarget.GetComponentInParent<ActorScript>().Dead)
				_actor.CurrentTarget = null;

			foreach(var weapon in _actor.Weapons)
			{
				if (_actor.TorsoAlignedToTarget && weapon.Ready && weapon.AutoFire && weapon.Range > destinationToTarget && weapon.Cost <= _actor.Energy)
					weapon.Fire(_actor.CurrentTarget);
			}
			
		}
	}
}