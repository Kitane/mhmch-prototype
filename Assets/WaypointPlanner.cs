using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaypointPlanner
{
	public class Waypoint {
		public Vector3 Position;
		public Transform Owner;
	};

	public Transform WaypointObject;

	LinkedList<Waypoint> Waypoints = new LinkedList<Waypoint>();
	
	public void AddWaypoint(Vector3 waypoint)
	{
		Transform owner = (Transform) Object.Instantiate(WaypointObject, waypoint, Quaternion.identity);
		Waypoints.AddLast(new Waypoint { Position = waypoint, Owner = owner });
	}

	public Waypoint PopWaypoint()
	{
		var wp = Waypoints.First.Value;
		Waypoints.RemoveFirst();
		return wp;
	}

	public bool MoreWaypoints
	{
		get { return Waypoints.Any(); }
	}


}
