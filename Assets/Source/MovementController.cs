using UnityEngine;
using System.Collections;


public class MovementController : MonoBehaviour {

	/*public WaypointPlanner.Waypoint CurrentWaypoint;
	public Transform WaypointObject;
	WaypointPlanner WaypointPlanner;
*/
	NavMeshAgent _navMeshAgent;

	public void SetDestination (Transform destination)
	{
		_navMeshAgent.SetDestination(destination.position);
	}

	void Start () 
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		//WaypointPlanner = new WaypointPlanner();
		//WaypointPlanner.WaypointObject = WaypointObject;
	}

	void Update () 
	{
		if (!GameManager.Instance.Running && Input.GetMouseButtonUp(0))
		{
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000)) 
				//WaypointPlanner.AddWaypoint(hit.point);
				_navMeshAgent.SetDestination(hit.point);

		}

		/*
		if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
		{
			if (CurrentWaypoint != null) {
				Object.Destroy(CurrentWaypoint.Owner.gameObject);
				CurrentWaypoint = null;
			}

			if( WaypointPlanner.MoreWaypoints)
			{
				CurrentWaypoint = WaypointPlanner.PopWaypoint();
				_navMeshAgent.SetDestination(CurrentWaypoint.Position);
			}
		}*/
	}


}
