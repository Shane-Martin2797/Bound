using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

	
	//We're going to have multiple input devices (one per player). This keeps track of which input devices are in us by players
	private static List<InControl.InputDevice> activeDevices = new List<InControl.InputDevice> ();
	public InControl.InputDevice inputDevice { get; private set; }
	public bool hasInputDevice { get { return inputDevice != null; } }
	
	
	private float movementSpeed = 1;
	public int team = 1;
	

	// Use this for initialization
	void Awake ()
	{
		//HUD.LoadForPlayer (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (inputDevice == null) {
			ScanForInputDevice ();
		} else {
			if (inputDevice.LeftStickX.Value != 0 || inputDevice.LeftStickY.Value != 0) {
				Movement ();
			}
		}
	}
	
	void Movement ()
	{
		float angle = Mathf.Rad2Deg * Mathf.Atan2 (inputDevice.LeftStickY.Value, inputDevice.LeftStickX.Value) - 90;
		GetComponent<Rigidbody2D> ().MoveRotation (angle);
		transform.Translate (Vector3.forward * movementSpeed);
	}
	
	void OnDestroy ()
	{
		ReleaseActiveInputDevice ();
	}
	
	void ReleaseActiveInputDevice ()
	{
		if (inputDevice != null && activeDevices.Contains (inputDevice)) {
			activeDevices.Remove (inputDevice);
		}
	}
	
	
	private void ScanForInputDevice ()
	{
		foreach (var device in InControl.InputManager.Devices) {
			if (!activeDevices.Contains (device)) {
				activeDevices.Add (device);
				inputDevice = device;
				return;
			}
		}
	}
	
}
