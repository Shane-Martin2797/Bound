using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject cameraP;
	public float movementSpeed = 10;
	public float rotationSpeed = 50;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (InControl.InputManager.ActiveDevice.LeftStickX.Value != 0 || InControl.InputManager.ActiveDevice.LeftStickY.Value != 0) {
			Movement ();
		}
		
		if (InControl.InputManager.ActiveDevice.RightStickX.Value != 0) {
			Rotation ();
		}
		
	}
	
	void Movement ()
	{
		Vector3 movement = new Vector3 (InControl.InputManager.ActiveDevice.LeftStickX.Value, 0, InControl.InputManager.ActiveDevice.LeftStickY.Value);
		
		transform.Translate (movement * movementSpeed * Time.deltaTime);
		/*
		Vector3 cameraRotationForward = new Vector3 (cameraP.transform.forward.x, 0, gameObject.transform.forward.z);
		Vector3 cameraRotationRight = new Vector3 (cameraP.transform.right.x, 0, gameObject.transform.right.z);
		
		cameraRotationForward *= (movementSpeed * InControl.InputManager.ActiveDevice.LeftStickY.Value * Time.deltaTime);
		cameraRotationRight *= (movementSpeed * InControl.InputManager.ActiveDevice.LeftStickX.Value * Time.deltaTime);
		
		transform.Translate (cameraRotationForward);
		transform.Translate (cameraRotationRight);
		*/
		
		
	}
	
	void Rotation ()
	{
		
		Vector3 rotationVector = new Vector3 (0, InControl.InputManager.ActiveDevice.RightStickX.Value * Time.deltaTime * rotationSpeed, 0);
		//cameraP.transform.RotateAround (transform.position, rotationVector.normalized, Mathf.Abs (rotationVector.y));
		
		transform.Rotate (rotationVector);
	}
}
