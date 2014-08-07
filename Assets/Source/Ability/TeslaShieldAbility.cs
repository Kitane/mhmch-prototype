using UnityEngine;
using System.Collections;

public class TeslaShieldAbility : MonoBehaviour
{
	public float _shieldRadius;
	public float _shieldDuration;

	public GameObject _shieldVisualisation;

	private float _timeElapsed;

	public void StartAbility()
	{
		if (_shieldVisualisation != null)
		{
			_shieldVisualisation.SetActive(true);//show tesla shield object
		}
	}
	
	public void Update()
	{
		if (_timeElapsed < _shieldDuration)
		{
			_timeElapsed += Time.deltaTime;//tick
		}
		else
		{
			_shieldVisualisation.SetActive(false);//hide tesal shield
		}
	}
}
