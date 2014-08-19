using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {

	public float DegreesPerSecond;

	ActorScript _actorScript;
	// Use this for initialization
	void Start () {
		_actorScript = GetComponentInParent<ActorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_actorScript.CurrentTarget != null)
			transform.Rotate(0, DegreesPerSecond * Time.deltaTime, 0);
	}
}
