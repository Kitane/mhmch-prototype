using UnityEngine;
using System.Collections;

public class UISwitchButton : UIButton
{
	private bool _isEnabled = true;

	void Start()
	{
		defaultColor = hover;
	}

	public override void OnPress (bool isPressed) 
	{ 
		if (isPressed)
		{
			_isEnabled = !_isEnabled;
			UpdateColor(_isEnabled, true);
		}
	}

	public bool SwitchState
	{
		get
		{
			return _isEnabled;
		}

		set
		{
			_isEnabled = value;
			UpdateColor(_isEnabled, true);
		}
	}
}
