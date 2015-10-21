using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

	public WallController targetWall;
	public WallController heldWall { get; private set; }
	
	//We're going to have multiple input devices (one per player). This keeps track of which input devices are in us by players
	private static List<InControl.InputDevice> activeDevices = new List<InControl.InputDevice> ();
	public InControl.InputDevice inputDevice { get; private set; }
	public bool hasInputDevice { get { return inputDevice != null; } }
	
	
	public Camera uiCamera;
	public float movementSpeed = 10;
	public float rotationSpeed = 50;
	

	// Use this for initialization
	void Awake ()
	{
		HUD.LoadForPlayer (this);
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
		
			if (inputDevice.RightStickX.Value != 0) {
				Rotation ();
			} else {
				this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			}
			CheckWalls ();
		}
		
		
	}
	
	void Movement ()
	{
		if (heldWall == null) {
			Vector3 movement = new Vector3 (inputDevice.LeftStickX.Value, 0, inputDevice.LeftStickY.Value);
			transform.Translate (movement * movementSpeed * Time.deltaTime);
		}
		/*
		Vector3 cameraRotationForward = new Vector3 (uiCamera.transform.forward.x, 0, gameObject.transform.forward.z);
		Vector3 cameraRotationRight = new Vector3 (uiCamera.transform.right.x, 0, gameObject.transform.right.z);
		
		cameraRotationForward *= (movementSpeed * InControl.InputManager.ActiveDevice.LeftStickY.Value * Time.deltaTime);
		cameraRotationRight *= (movementSpeed * InControl.InputManager.ActiveDevice.LeftStickX.Value * Time.deltaTime);
		
		transform.Translate (cameraRotationForward);
		transform.Translate (cameraRotationRight);
		*/
		
		
	}
	
	void Rotation ()
	{
		if (heldWall == null) {
			Vector3 rotationVector = new Vector3 (0, inputDevice.RightStickX.Value * Time.deltaTime * rotationSpeed, 0);
			transform.Rotate (rotationVector);
		} 
		
		//uiCamera.transform.RotateAround (transform.position, rotationVector.normalized, Mathf.Abs (rotationVector.y));
	}
	
	
	void CheckWalls ()
	{
		if (heldWall != null) {
			if (inputDevice.Action1.IsPressed) {
				heldWall = null;
			}
		} else if (targetWall != null) {
			if (RayCastCheck ()) {
				if (inputDevice.Action1.IsPressed) {
					heldWall = targetWall;
				}
			}
		} 
			
	}
	
	bool RayCastCheck ()
	{
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.forward, out hit, 1f);
		if (hit.collider != null) {
			if (hit.collider.gameObject.GetComponent<WallController> () == targetWall) {
				return true;
			}
		}
		return false;
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
	
	public void resetTargetWall ()
	{
		targetWall = null;
	}
	public void setTargetWall (WallController wall)
	{
		targetWall = wall;
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
