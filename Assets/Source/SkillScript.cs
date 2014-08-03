using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillScript : MonoBehaviour
{
	public string SkillName;
	public float ReloadSpeed;
	public float Cost;
	public bool Ready { get { return _remainingReloadSpeed <= 0.0f; } }

	float _remainingReloadSpeed;
	ActorScript _actor;
	
	public void Use(GameObject target)
	{
		_remainingReloadSpeed = ReloadSpeed;
		_actor.Energy -= Cost;
	}

	void Start()
	{
		_actor = GetComponentInParent<ActorScript>();
	}


	void Update()
	{
		if (_remainingReloadSpeed > 0.0)
			_remainingReloadSpeed -= Time.deltaTime;
	}


}
