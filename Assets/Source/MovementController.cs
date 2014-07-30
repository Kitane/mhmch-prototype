using UnityEngine;
using System.Collections;


public class MovementController : MonoBehaviour {

	/*public WaypointPlanner.Waypoint CurrentWaypoint;
	public Transform WaypointObject;
	WaypointPlanner WaypointPlanner;
*/
	NavMeshAgent _navMeshAgent;
	public GameObject goTerrain;

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
			/*
			if (Physics.Raycast (ray, out hit, 1000))
			{
				//WaypointPlanner.AddWaypoint(hit.point);
				_navMeshAgent.SetDestination(hit.point);
			}*/

			GameObject clickedObject = GetClickedGameObject(out hit);

			//if collision with terrain
			if (clickedObject == goTerrain)
			{
				_navMeshAgent.SetDestination(hit.point);//go on new position
			}
			else if (clickedObject != null)
			{
				//ok we have collision with object
				Debug.Log("Object clicked:" + clickedObject.name);
			}
		}
	}

	GameObject GetClickedGameObject(out RaycastHit hit)
	{ 
		// Builds a ray from camera point of view to the mouse position 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Casts the ray and get the first game object hit 
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			return hit.transform.gameObject;
		}
		else
		{
			return null;
		}
	}
}
