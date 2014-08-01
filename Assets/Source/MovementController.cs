using UnityEngine;
using System.Collections;


public class MovementController : MonoBehaviour {

	/*public WaypointPlanner.Waypoint CurrentWaypoint;
	public Transform WaypointObject;
	WaypointPlanner WaypointPlanner;
*/
	NavMeshAgent _navMeshAgent;
	public GameObject goTerrain;

	int IgnoreLayerMask;

	ActorScript _actor;

	void Start () 
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		//WaypointPlanner = new WaypointPlanner();
		//WaypointPlanner.WaypointObject = WaypointObject;
		IgnoreLayerMask = LayerMask.NameToLayer("Sensors");
		_actor = GetComponent<ActorScript>();
	}

	void Update () 
	{
		if (!GameManager.Instance.Running && Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			GameObject clickedObject = GetClickedGameObject(out hit);

			//if collision with terrain
			if (clickedObject == goTerrain)
			{
				_actor.SetDestination(hit.point);//go on new position
			}
			else if (clickedObject != null)
			{
				var target = clickedObject.GetComponentInParent<ActorScript>();
				//ok we have collision with object
				if (target != null && target.ActorTeam != _actor.ActorTeam && !_actor.Dead) {
					_actor.CurrentTarget = target.gameObject.transform.Find("Hitzone");
				}
				Debug.Log("Object clicked:" + clickedObject.name);
			}
		}
	}

	GameObject GetClickedGameObject(out RaycastHit hit)
	{ 
		// Builds a ray from camera point of view to the mouse position 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


		// Casts the ray and get the first game object hit 
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, IgnoreLayerMask))
		{
			return hit.transform.gameObject;
		}
		else
		{
			return null;
		}
	}
}
