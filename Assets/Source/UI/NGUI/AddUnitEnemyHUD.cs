using UnityEngine;

/// <summary>
/// Example script that instantiates a HUD window that will follow this game object.
/// </summary>

[AddComponentMenu("NGUI/Examples/Add Unit Enemy HUD")]
public class AddUnitEnemyHUD : MonoBehaviour
{
	public GameObject prefab;
	public ActorScript actorScript;
	
	void Start ()
	{
		if (prefab != null)
		{
			UICamera cam = UICamera.FindCameraForLayer(prefab.layer);
			
			if (cam != null)
			{
				GameObject go = cam.gameObject;
				UIAnchor anchor = go.GetComponent<UIAnchor>();
				if (anchor != null) go = anchor.gameObject;
				
				GameObject child = GameObject.Instantiate(prefab) as GameObject;
				Transform t = child.transform;
				t.parent = go.transform;
				t.localPosition = Vector3.zero;
				t.localRotation = Quaternion.identity;
				t.localScale = Vector3.one;
				
				UnitEnemyHUD hud = child.GetComponent<UnitEnemyHUD>();
				if (hud != null)
				{
					hud.target = transform;
					hud.actorScript = actorScript;
				}
			}
			else
			{
				Debug.LogWarning("No camera found for layer " + LayerMask.LayerToName(prefab.layer), gameObject);
			}
		}
		Destroy(this);
	}
}