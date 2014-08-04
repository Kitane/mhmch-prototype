using UnityEngine;
using System.Collections;

public class DamageScriptObstacle : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		var projectile = other.gameObject.GetComponentInParent<ProjectileScript>();
		if (projectile != null)
		{
			Debug.Log("obstacle damage:" + projectile.Definition.Damage);
			projectile.Impact(other.gameObject.transform.position);
		}
	}
}
