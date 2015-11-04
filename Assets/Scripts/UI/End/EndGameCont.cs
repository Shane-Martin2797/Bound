using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndGameCont : MonoBehaviour
{
	
	
	void Update ()
	{
		if (InControl.InputManager.ActiveDevice.Action1.WasPressed) {
			LoadTitle ();
		}
	}
	
	void LoadTitle ()
	{
		Application.LoadLevel (Scenes.Title);
	}
	
	
	
}