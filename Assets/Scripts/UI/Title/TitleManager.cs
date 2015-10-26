using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleManager : MonoBehaviour
{

	private static List<InControl.InputDevice> activeDevices = new List<InControl.InputDevice> ();
	
	public InControl.InputDevice inputDevice1 { get; private set; }
	public InControl.InputDevice inputDevice2 { get; private set; }
	public InControl.InputDevice inputDevice3 { get; private set; }
	public InControl.InputDevice inputDevice4 { get; private set; }
	
	
	void Update ()
	{
		if (inputDevice1 == null) {
			ScanForInputDevice1 ();
		} else {
			Debug.Log ("Input 1 Found");
		}
		if (inputDevice2 == null) {
			ScanForInputDevice2 ();
		} else {
			Debug.Log ("Input 2 Found");
		}
		if (inputDevice3 == null) {
			ScanForInputDevice3 ();
		} else {
			Debug.Log ("Input 3 Found");
		}
		if (inputDevice4 == null) {
			ScanForInputDevice4 ();
		} else {
			Debug.Log ("Input 4 Found");
		}
		if (inputDevice1 != null && inputDevice2 != null && inputDevice3 != null && inputDevice4 != null) {
			if (inputDevice1.Action1.WasPressed && inputDevice2.Action1.WasPressed && inputDevice3.Action1.WasPressed && inputDevice4.Action1.WasPressed) {
				StartGame ();
			}
		}
	}

	void StartGame ()
	{
		Application.LoadLevel (Scenes.Maze);
	}
	
	
	
	private void ScanForInputDevice1 ()
	{
		foreach (var device in InControl.InputManager.Devices) {
			if (!activeDevices.Contains (device)) {
				activeDevices.Add (device);
				inputDevice1 = device;
				return;
			}
		}
	}
	
	private void ScanForInputDevice2 ()
	{
		foreach (var device in InControl.InputManager.Devices) {
			if (!activeDevices.Contains (device)) {
				activeDevices.Add (device);
				inputDevice2 = device;
				return;
			}
		}
	}
	
	private void ScanForInputDevice3 ()
	{
		foreach (var device in InControl.InputManager.Devices) {
			if (!activeDevices.Contains (device)) {
				activeDevices.Add (device);
				inputDevice3 = device;
				return;
			}
		}
	}
	
	private void ScanForInputDevice4 ()
	{
		foreach (var device in InControl.InputManager.Devices) {
			if (!activeDevices.Contains (device)) {
				activeDevices.Add (device);
				inputDevice4 = device;
				return;
			}
		}
	}
	
	void ReleaseActiveInputDevice1 ()
	{
		if (inputDevice1 != null && activeDevices.Contains (inputDevice1)) {
			activeDevices.Remove (inputDevice1);
		}
	}
	void ReleaseActiveInputDevice2 ()
	{
		if (inputDevice1 != null && activeDevices.Contains (inputDevice2)) {
			activeDevices.Remove (inputDevice2);
		}
	}
	void ReleaseActiveInputDevice3 ()
	{
		if (inputDevice1 != null && activeDevices.Contains (inputDevice3)) {
			activeDevices.Remove (inputDevice3);
		}
	}
	void ReleaseActiveInputDevice4 ()
	{
		if (inputDevice1 != null && activeDevices.Contains (inputDevice4)) {
			activeDevices.Remove (inputDevice4);
		}
	}
	
}
