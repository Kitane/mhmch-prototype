using UnityEngine;
using System.Collections;

public class TeslaShieldAbility : Ability
{
	public float _shieldRadius;
	public GameObject _shieldVisualisation;
	public UIFilledSprite _shieldUIVisualisation;

	private bool _running;
	private float _timeElapsed;
	private float _reloadTimeElapsed;

	public override void StartAbility()
	{
		if (_shieldVisualisation != null)
		{
			_shieldVisualisation.SetActive(true);//show tesla shield object
		}

		_running = true;
		_timeElapsed = 0;
	}
	
	public void Update()
	{
		if (_running)
		{
			if (_timeElapsed < _durationTime)
			{
				_timeElapsed += Time.deltaTime;//tick
				UpdateUI(1 - (_timeElapsed / _durationTime));
			}
			else
			{
				_running = false;
				_reloadTimeElapsed = 0;
				_shieldVisualisation.SetActive(false);//hide tesla shield
			}
		}
		else if (_reloadTimeElapsed < _reloadTime)
		{
			_reloadTimeElapsed += Time.deltaTime;//tick for charge
			UpdateUI(_reloadTimeElapsed / _reloadTime);
		}
	}

	private void UpdateUI(float progress)
	{
		if (_shieldUIVisualisation != null)
		{
			_shieldUIVisualisation.fillAmount = progress;
		}
	}

	public override bool IsReady()
	{
		return (_reloadTimeElapsed >= _reloadTime) && !_running;
	}
}
