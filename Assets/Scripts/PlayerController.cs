﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{	
	[System.Serializable]
	public class SpellBook
	{
		public Spell LBSpell;
		public Spell RBSpell;
		public Spell RTSpell;
		public Spell LTSpell;
	}
	
	//We're going to have multiple input devices (one per player). This keeps track of which input devices are in us by players
	private static List<InControl.InputDevice> activeDevices = new List<InControl.InputDevice> ();
	public InControl.InputDevice inputDevice { get; private set; }
	public bool hasInputDevice { get { return inputDevice != null; } }
	
	
	public SpellBook spellBook;
	public int team = 0;
	public float dashDistance = 10;
	public LayerMask layers;
	public Transform respawnPoint;
	private float movementSpeed = 10;
	public float health = 120;
	public float maxHealth = 120;
	public float castTime = 0;
	
	void Awake ()
	{
		health = maxHealth;
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
			if (inputDevice.RightStickX.Value != 0 || inputDevice.RightStickY.Value != 0) {
				Rotation ();
			}
			Inputs ();
		}
	}
	
	void Inputs ()
	{
		//Left Bumper
		{
			if (spellBook.LBSpell != null) {
				if (inputDevice.LeftBumper.WasPressed) {
					spellBook.LBSpell.PressCast ();
				}
				if (inputDevice.LeftBumper.IsPressed) {
					spellBook.LBSpell.HoldCast ();
				}
				if (inputDevice.LeftBumper.WasReleased) {
					spellBook.LBSpell.ReleaseCast ();
				}
			}
		}
		//Right Bumper
		{
			if (spellBook.RBSpell != null) {
				if (inputDevice.RightBumper.WasPressed) {
					spellBook.RBSpell.PressCast ();
				}
				if (inputDevice.RightBumper.IsPressed) {
					spellBook.RBSpell.HoldCast ();
				}
				if (inputDevice.RightBumper.WasReleased) {
					spellBook.RBSpell.ReleaseCast ();
				}
			}
		}
		//Left Trigger
		{
			if (spellBook.LTSpell != null) {
				if (inputDevice.LeftTrigger.WasPressed) {
					spellBook.LTSpell.PressCast ();
					
				}
				if (inputDevice.LeftTrigger.IsPressed) {
					spellBook.LTSpell.HoldCast ();
					
				}
				if (inputDevice.LeftTrigger.WasReleased) {
					spellBook.LTSpell.ReleaseCast ();
				}
			}
		}
		//Right Trigger
		{
			if (spellBook.RTSpell != null) {
				if (inputDevice.RightTrigger.WasPressed) {
					spellBook.RTSpell.PressCast ();
				}
				if (inputDevice.RightTrigger.IsPressed) {
					spellBook.RTSpell.HoldCast ();
				}
				if (inputDevice.RightTrigger.WasReleased) {
					spellBook.RTSpell.ReleaseCast ();
				}
			}
		}
	}
	
	void Movement ()
	{
		Vector3 pos = transform.position;
		Vector3 movementDirection = new Vector3 (inputDevice.LeftStickX.Value, inputDevice.LeftStickY.Value, 0) * movementSpeed * Time.deltaTime;
		pos += movementDirection;
		transform.position = pos;
	}
	
	void Rotation ()
	{
		float angle = Mathf.Rad2Deg * Mathf.Atan2 (inputDevice.RightStickY.Value, inputDevice.RightStickX.Value) - 90;
		GetComponent<Rigidbody2D> ().MoveRotation (angle);
	}
	
	//This also heals player, just make amount -ve
	public void DamagePlayer (float amount)
	{
		health -= amount;
		if (health < 0) {
			PlayerDied ();
		}
		if (health > maxHealth) {
			health = maxHealth;
		}
	}
	
	
	public Vector3 dashTargetPosition;
	public void DashSetup ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, dashDistance, layers.value);
		if (hit != null) {
			if (hit.collider != null) {
				dashTargetPosition = hit.collider.bounds.ClosestPoint (transform.position);
			} else {
				dashTargetPosition = (transform.position + (transform.up * dashDistance));
			}
		} else {
			dashTargetPosition = (transform.position + (transform.up * dashDistance));
		}
		Dash ();
	}
	
	void Dash ()
	{
		transform.position = dashTargetPosition;
	}
	
	
	void PlayerDied ()
	{
		if (team == 1) {
			GameController.Instance.Team1LosesLife ();
		} else if (team == 2) {
			GameController.Instance.Team2LosesLife ();
		} else {
			Debug.LogWarning ("No Team Set");
		}
		transform.position = respawnPoint.position;
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
