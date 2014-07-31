using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileScript : MonoBehaviour
{
	public ProjectileAttributes Definition;
	public GameObject Target;
	public float RemainingTime;

	void Update()
	{
		transform.Translate(Vector3.forward * Definition.Speed * Time.deltaTime);
		if (Definition.TrackingSpeed > 0)
		{
			var targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
			Quaternion.RotateTowards(transform.rotation, targetRotation, Definition.TrackingSpeed);
		}
		RemainingTime -= Time.deltaTime;
		if (RemainingTime <= 0)
			Destroy(gameObject);
	}
}
