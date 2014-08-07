using UnityEngine;
using System.Collections;

/**
 * Basic interface for all abilities.
 * 
 * */
public class Ability : MonoBehaviour
{
	public float _durationTime;
	public float _reloadTime;

	public virtual void StartAbility() {}
	public virtual bool IsReady() { return false;}
}
