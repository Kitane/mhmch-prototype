using UnityEngine;
using System.Collections;


public class MahouScript : MonoBehaviour {

	public Vector3 OverviewCameraPosition;
	public Vector3 OverviewCameraRotation;
	public float OverviewFOV;

	public Vector3 ThirdPersonCameraPosition;
	public Vector3 ThirdPersonCameraRotation;
	public float ThirdPersonFOV;

	public WaypointPlanner.Waypoint CurrentWaypoint;

	public Transform WaypointObject;
	WaypointPlanner WaypointPlanner;

	NavMeshAgent _navMeshAgent;

	public void SetDestination (Transform destination)
	{
		_navMeshAgent.SetDestination(destination.position);
	}

	void Start () 
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		WaypointPlanner = new WaypointPlanner();
		WaypointPlanner.WaypointObject = WaypointObject;
		GameManager.Instance.ModeChanged += HandleModeChanged;
	}

	void HandleModeChanged(GameManager.GameModes newMode, GameManager.GameModes oldMode)
	{
		if (newMode == GameManager.GameModes.ThirdPerson)
		{
			_navMeshAgent.Resume();
			SetCameraToThirdPerson();
		}
		else if (newMode == GameManager.GameModes.Overview)
		{
			_navMeshAgent.Stop (true);
			SetCameraToOverview();
		}
	}

	void Update () 
	{
		if (GameManager.Instance.Paused && Input.GetMouseButtonUp(0))
		{
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000)) { 
				WaypointPlanner.AddWaypoint(hit.point);
				Debug.Log("Map coordinates: " + hit.point);
			}
		}

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
		}
	}

	void SetCameraToThirdPerson()
	{
		Camera.main.transform.localPosition = ThirdPersonCameraPosition;
		Camera.main.transform.localEulerAngles = ThirdPersonCameraRotation;
		Camera.main.fieldOfView = ThirdPersonFOV;
	}

	void SetCameraToOverview()
	{
		Camera.main.transform.localPosition = OverviewCameraPosition;
		Camera.main.transform.eulerAngles = OverviewCameraRotation;
		//Camera.main.transform.localEulerAngles = OverviewCameraRotation;
		Camera.main.fieldOfView = OverviewFOV;
	}

}
