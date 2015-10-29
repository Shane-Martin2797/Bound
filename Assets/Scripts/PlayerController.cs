using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public LineRenderer lineRenderer;
	
	//We're going to have multiple input devices (one per player). This keeps track of which input devices are in us by players
	private static List<InControl.InputDevice> activeDevices = new List<InControl.InputDevice> ();
	public InControl.InputDevice inputDevice { get; private set; }
	public bool hasInputDevice { get { return inputDevice != null; } }
	
	private float defaultMovmentSpeed = 10;
	private float closeMovementSpeed = 15;
	
	void OnTriggerEnter2D (Collider2D col)
	{
		PlayerController collidedPlayer = col.GetComponent<PlayerController> ();
		if (collidedPlayer != null) {
			if (collidedPlayer.team == this.team) {
				collidedPlayer.SpeedUp ();
				if (lineRenderer != null) {
					AddLine ();
				}
			}
		}
	}
	void OnTriggerExit2D (Collider2D col)
	{
		PlayerController collidedPlayer = col.GetComponent<PlayerController> ();
		if (collidedPlayer != null) {
			if (collidedPlayer.team == this.team) {
				collidedPlayer.SlowDown ();
				if (lineRenderer != null) {
					RemoveLine ();
				}
			}
		}
	}
	
	public void SpeedUp ()
	{
		movementSpeed = closeMovementSpeed;
	}
	public void SlowDown ()
	{
		movementSpeed = defaultMovmentSpeed;
	}
	
	public float movementSpeed = 10f;
	public int team = 1;
	public bool canMove = true;
	

	// Use this for initialization
	void Awake ()
	{
		defaultMovmentSpeed = movementSpeed;
		closeMovementSpeed = movementSpeed * 1.5f;
		lineRenderer = this.GetComponent<LineRenderer> ();
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
		
		if (lineRenderer != null) {
			if (lineRenderer.enabled) {
				lineRenderer.SetPosition (0, transform.position);
				if (this.GetComponent<PushControl> () != null) {
					for (int i = 0; i < GameController.Instance.GhostList.Count; ++i) {
						if (GameController.Instance.GhostList [i].player.team == this.team) {
							lineRenderer.SetPosition (1, GameController.Instance.GhostList [i].transform.position);
						}
					}
				} else if (this.GetComponent<GhostControl> () != null) {
					for (int i = 0; i < GameController.Instance.HumanList.Count; ++i) {
						if (GameController.Instance.HumanList [i].player.team == this.team) {
							lineRenderer.SetPosition (1, GameController.Instance.HumanList [i].transform.position);
						}
					}
				}
				
			}
		}
	}
	
	void Movement ()
	{
		if (canMove) {
			float angle = Mathf.Rad2Deg * Mathf.Atan2 (inputDevice.LeftStickY.Value, inputDevice.LeftStickX.Value) - 90;
			GetComponent<Rigidbody2D> ().MoveRotation (angle);
			transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
		}
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
	
	void AddLine ()
	{
		if (lineRenderer != null) {
			lineRenderer.enabled = true;
			lineRenderer.SetVertexCount (2);
		}
	}
	void RemoveLine ()
	{
		if (lineRenderer != null) {
			lineRenderer.enabled = false;
		}
	}
}
