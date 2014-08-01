using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	ActorScript _actor;
	// Use this for initialization
	void Start () {
		_actor = GetComponentInParent<ActorScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		var projectile = other.gameObject.GetComponentInParent<ProjectileScript>();
		if (projectile != null)
		{
			Debug.Log("damage taken: " + projectile.Definition.Damage);
			_actor.ReceiveDamage(projectile.Definition.Damage);
			projectile.Impact(other.gameObject.transform.position);
		}
	}
}
