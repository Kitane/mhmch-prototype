using UnityEngine;
using System.Collections;

public class MahouScript : MonoBehaviour {

	NavMeshAgent navMeshAgent;

	public Transform Destination;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		navMeshAgent.SetDestination(Destination.position);
	}
}
