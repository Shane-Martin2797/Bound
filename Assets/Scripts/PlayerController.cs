using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{	
	bool offSpawn = false;
	bool canMove = true;
	float spawnTimer = 3;
	float force = 500;
	float dashDamage = 20;
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
	private float dashSpeed = 80;
	private float health = 120;
	public float maxHealth = 120;
	public float castTime = 0;
	public GameObject healthBar;
	
	void Awake ()
	{
		DamagePlayer (-maxHealth);
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		if (inputDevice == null) {
			ScanForInputDevice ();
		} else {
			if (inputDevice.LeftStickX.Value != 0 || inputDevice.LeftStickY.Value != 0) {
				if (canMove) {
					Movement ();
				}
			}
			if (inputDevice.RightStickX.Value != 0 || inputDevice.RightStickY.Value != 0) {
				Rotation ();
			}
			if (!offSpawn) {
				Inputs ();
			}
		}
		if (offSpawn) {
			spawnTimer -= Time.deltaTime;
			if (spawnTimer < 0) {
				offSpawn = false;
			}
		}
		if (lerping) {
			LerpDash ();
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
		this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		Vector3 pos = transform.position;
		Vector3 movementDirection = new Vector3 (inputDevice.LeftStickX.Value, inputDevice.LeftStickY.Value, 0) * movementSpeed * Time.deltaTime;
		pos += movementDirection;
		transform.position = pos;
	}
	
	void Rotation ()
	{
		this.GetComponent<Rigidbody2D> ().angularVelocity = 0;
		float angle = Mathf.Rad2Deg * Mathf.Atan2 (inputDevice.RightStickY.Value, inputDevice.RightStickX.Value) - 90;
		GetComponent<Rigidbody2D> ().MoveRotation (angle);
	}
	
	//This also heals player, just make amount -ve
	public void DamagePlayer (float amount)
	{
		if (!offSpawn) {
			health -= amount;
			if (health <= 0) {
				PlayerDied ();
			}
			if (health > maxHealth) {
				health = maxHealth;
			}
			healthBar.transform.localScale = new Vector3 (health / maxHealth, 1, 1);
		}
	}
	
	
	public Vector3 dashTargetPosition;
	public void DashSetup ()
	{
		float posZ = transform.position.z;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, dashDistance, layers.value);
		if (hit != null) {
			if (hit.collider != null) {
				if (hit.collider.GetComponent<PolygonCollider2D> () != null) {
					dashTargetPosition = FindClosestPolygonPoint2D (hit);
				} else {
					dashTargetPosition = hit.collider.bounds.ClosestPoint (transform.position);
				}
			} else {
				dashTargetPosition = (transform.position + (transform.up * dashDistance));
			}
		} else {
			dashTargetPosition = (transform.position + (transform.up * dashDistance));
		}
		dashTargetPosition.z = posZ;
		Dash ();
	}
	
	
	Vector3 FindClosestPolygonPoint2D (RaycastHit2D hit)
	{
		PolygonCollider2D col = hit.collider.GetComponent<PolygonCollider2D> ();
		float minDistanceSqr = Mathf.Infinity;
		Vector3 nearestColliderPoint = Vector3.zero;
		
		// Scan all collider points to find nearest
		foreach (Vector3 colliderPoint in col.points) {
			// Convert to world point
			Vector3 colliderPointWorld = hit.transform.TransformPoint (colliderPoint);
			
			Vector3 diff = hit.point - (Vector2)colliderPointWorld;
			float distSqr = diff.sqrMagnitude;
			
			if (distSqr < minDistanceSqr) {
				minDistanceSqr = distSqr;
				nearestColliderPoint = colliderPointWorld;
			}
		}
		
		return nearestColliderPoint;
	}
	
	bool lerping = false;
	float timer = 0;
	float distance = 0;
	float totalTime = 0;
	Vector3 myPosition;
	
	void Dash ()
	{
		distance = Vector3.Distance (transform.position, dashTargetPosition);
		totalTime = distance / dashSpeed;
		myPosition = transform.position;
		lerping = true;
		canMove = false;
		timer = 0;
	}
	
	void LerpDash ()
	{
		timer += Time.deltaTime / totalTime;
		Vector3 pos = Vector3.Lerp (myPosition, dashTargetPosition, timer);
		transform.position = pos;
		if (timer >= 1) {
			lerping = false;
			canMove = true;
		}
	}
	
	void PlayerDied ()
	{
		if (GameController.Instance != null) {
			if (team == 1) {
				GameController.Instance.Team1LosesLife ();
				Debug.Log ("Team 1");
			} else if (team == 2) {
				GameController.Instance.Team2LosesLife ();
			} else {
				Debug.LogWarning ("No Team Set");
			}
		}
		Respawn ();
	}
	
	public void Respawn ()
	{
		transform.position = respawnPoint.position + new Vector3 (Random.Range (-3, 4), Random.Range (-3, 4), 0);
		DamagePlayer (-maxHealth);
		offSpawn = true;
		spawnTimer = 3;
	}
	
	void OnDestroy ()
	{
		Debug.Log ("Destroyed");
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
