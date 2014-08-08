using UnityEngine;
using System.Collections;

[AddComponentMenu("NGUI/UI/Switch Image Button")]
public class UISwitchImageButton : UIImageButton
{
	private bool _isEnabled = true;

	void Start()
	{

	}

	public void OnPress (bool isPressed) 
	{ 
		if (isPressed)
		{
			SetRightImage();

			_isEnabled = !_isEnabled;
		}
	}

	void OnHover (bool isOver)
	{

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
			SetRightImage();
		}
	}

	private void SetRightImage()
	{
		if (_isEnabled)
		{
			target.spriteName = normalSprite;
		}
		else
		{
			target.spriteName = disabledSprite;
		}
		
		target.MakePixelPerfect();
	}
}
