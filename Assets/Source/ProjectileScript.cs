using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileScript : MonoBehaviour
{
	public ProjectileAttributes Definition;
	GameObject _target;

	void Update()
	{
		transform.Translate(Vector3.forward * Definition.Speed * Time.deltaTime);
		if (TrackingSpeed > 0)
		{
			var targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
			Quaternion.RotateTowards(transform.rotation, targetRotation, Definition.TrackingSpeed);
		}
	}
}
