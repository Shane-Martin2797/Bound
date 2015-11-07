using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleManager : MonoBehaviour
{

	void Update ()
	{
		if (InControl.InputManager.ActiveDevice.Action1.WasPressed) {
			StartGame ();
		}
	}

	void StartGame ()
	{
		Application.LoadLevel (Scenes.World);
	}
	
	
}
