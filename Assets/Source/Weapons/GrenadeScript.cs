using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeScript : MonoBehaviour
{
	public ProjectileAttributes Definition;
	public Transform Target;
	public int Team;
	public float RemainingTime;

	public void Impact(Vector3 location)
	{
		Destroy(gameObject);
	}

	void Update()
	{
		transform.Translate(Vector3.forward * Definition.Speed * Time.deltaTime);
		if (Definition.TrackingSpeed > 0.0f)
		{
			var targetRotation = Quaternion.LookRotation(Target.position - transform.position);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Definition.TrackingSpeed * Time.deltaTime);
		}

		RemainingTime -= Time.deltaTime;
		if (RemainingTime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
