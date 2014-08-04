using UnityEngine;
using System.Collections;

public class TeslaShield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Tesla shield collider:" + other.gameObject.name);

		var projectile = other.gameObject.GetComponentInParent<ProjectileScript>();
		if (projectile != null)
		{
			Debug.Log("destroy projektile: " + projectile.Definition.Damage + " name:" + other.gameObject.name);
			projectile.Impact(other.gameObject.transform.position);
		}
	}
}
