using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProjectileScript : MonoBehaviour
{
	public ProjectileAttributes Definition;
	public Transform Target;
	public int Team;
	public float RemainingTime;
	public Transform Quad;



	public void Impact(Vector3 location)
	{
		Destroy(gameObject);
	}

	void Start()
	{
		Quad = transform.Find("Model");
	}

	void Update()
	{
		transform.Translate(Vector3.forward * Definition.Speed * Time.deltaTime);
		if (Definition.TrackingSpeed > 0.0f)
		{
			var targetRotation = Quaternion.LookRotation(Target.position - transform.position);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Definition.TrackingSpeed * Time.deltaTime);
		}
		if (Quad != null)
		{
		
			Quad.transform.forward = - Camera.main.transform.forward;
			Quad.Rotate(0, transform.rotation.eulerAngles.y, 0);

			//Quad.transform.LookAt(transform.forward + transform.position, (Quad.transform.position - Camera.main.transform.position).normalized);
		}


		RemainingTime -= Time.deltaTime;
		if (RemainingTime <= 0)
			Destroy(gameObject);
	}
}
