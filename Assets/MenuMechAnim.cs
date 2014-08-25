using UnityEngine;
using System.Collections;

public class MenuMechAnim : MonoBehaviour {

	public Animator _mechAnimator;

	// Use this for initialization
	void Start () {
		if (_mechAnimator != null)
			_mechAnimator.SetBool("stop", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
