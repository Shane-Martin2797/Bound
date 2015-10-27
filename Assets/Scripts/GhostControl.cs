using UnityEngine;
using System.Collections;

public class GhostControl : MonoBehaviour
{
	public PlayerController player;
	public PlayerController heldPlayer;
	public LayerMask layers;
	
	private bool grabbed = false;
	private bool inWall = false;
	private bool moveTowardTeam = false;
	
	private float timer;
	private float defaultTimer = .25f;
	private float holdTimer = 3;
	private float holdTimerDefault = 3;
	
	private bool stunned;
	private float stunTimer;
	private float stunTimerDefault = 1f;
	private float stunDelay;
	private float stunDelayDefault = 3;
	
	private float grabDelay;
	private float grabDelayDefault = 2;
	
	void OnCollisionEnter2D (Collision2D col)
	{
		PlayerController touchedPlayer = col.gameObject.GetComponent<PlayerController> ();
		GhostControl touchedGhost = col.gameObject.GetComponent<GhostControl> ();
		if (col.gameObject.GetComponent<GhostWall> () != null) {
			TouchedWall ();
		} else if (touchedPlayer != null) {
			if (touchedGhost == null) {
				//This line makes it so you can't grab your teammate, It is gone now, due to the mechanic we want in our game...
				if (grabDelay < 0) {
					touchedPlayer.canMove = false;
					heldPlayer = touchedPlayer;
					grabbed = true;
					
					if (touchedPlayer.team != player.team) {
						GrabbedPlayerE ();
					} else {
						GrabbedPlayerT ();
					}
					holdTimer = holdTimerDefault;
				}
			}
		}
	}
	


	public void TouchedWall ()
	{
		GetComponent<Collider2D> ().enabled = false;
		inWall = true;
		timer = defaultTimer;
	}
	
	void GrabbedPlayerE ()
	{
		if (!stunned) {
			heldPlayer.transform.parent = this.transform;
			heldPlayer.GetComponent<Collider2D> ().enabled = false;
			for (int i = 0; i < GameController.Instance.GhostList.Count; ++i) {
				if (GameController.Instance.GhostList [i] != this) {
					GameController.Instance.GhostList [i].MoveTowardsTeammate ();
				}
			}
		}
	}
	
	void GrabbedPlayerT ()
	{
		if (!stunned) {
			heldPlayer.transform.parent = this.transform;
			heldPlayer.GetComponent<Collider2D> ().enabled = false;
		}
	}
	
	
	void LetGoOfPlayer ()
	{
		grabbed = false;
		heldPlayer.transform.parent = null;
		heldPlayer.GetComponent<Collider2D> ().enabled = true;
		heldPlayer.canMove = true;
		heldPlayer = null;
		grabDelay = grabDelayDefault;
		for (int i = 0; i < GameController.Instance.GhostList.Count; ++i) {
			if (GameController.Instance.GhostList [i] != this) {
				GameController.Instance.GhostList [i].StopMoveTowardTeammate ();
			}
		}
	}
	public void LeftWall ()
	{
		GetComponent<Collider2D> ().enabled = true;
	}
	
	
	void Awake ()
	{
		//	player = this.GetComponent<PlayerController> ();
		//	GameController.Instance.GhostList.Add (this);
	}
	// Use this for initialization
	void Start ()
	{
		player = this.GetComponent<PlayerController> ();
		GameController.Instance.GhostList.Add (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (inWall) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				inWall = false;
				LeftWall ();
			}
		}
		
		if (grabbed) {
			holdTimer -= Time.deltaTime;
			if (holdTimer < 0) {
				LetGoOfPlayer ();
			}
		}
		
		if (stunned) {
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0) {
				Free ();
			}
			if (heldPlayer != null) {
				LetGoOfPlayer ();
			}
		}
		
		if (stunDelay >= 0) {
			stunDelay -= Time.deltaTime;
		}
		
		if (grabDelay >= 0) {
			grabDelay -= Time.deltaTime;
		}
		
		if (moveTowardTeam) {
			MoveTeam ();
		}
		
		CheckWall ();
	}
	
	public void Stun ()
	{
		if (stunDelay <= 0) {
			stunned = true;
			player.canMove = false;
			stunTimer = stunTimerDefault;
			stunDelay = stunDelayDefault;
		}
	}
	void Free ()
	{
		stunned = false;
		player.canMove = true;
		stunDelay = stunDelayDefault;
	}
	
	public void MoveTowardsTeammate ()
	{
		player.canMove = false;
		moveTowardTeam = true;
		
	}
	
	public void StopMoveTowardTeammate ()
	{
		player.canMove = true;
		moveTowardTeam = false;
	}
	
	PlayerController teammate;
	void MoveTeam ()
	{
	
		if (teammate == null) {
			for (int i = 0; i < GameController.Instance.HumanList.Count; ++i) {
				if (GameController.Instance.HumanList [i].player.team == player.team) {
					teammate = GameController.Instance.HumanList [i].player;
				}
			}
		} 
		if (teammate != null) {
			Vector2 direction = (teammate.transform.position - transform.position).normalized;
			float angle = Mathf.Rad2Deg * Mathf.Atan2 (direction.y, direction.x) - 90;
			GetComponent<Rigidbody2D> ().MoveRotation (angle);
			transform.Translate (Vector3.forward * player.movementSpeed * Time.deltaTime);
		}
	}
	
	void CheckWall ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.forward, 1, layers.value);
		Debug.DrawRay (transform.position, transform.forward, Color.blue);
		if (hit != null) {
			if (hit.collider != null) {
				GhostWall wall = hit.collider.GetComponent<GhostWall> ();
				if (wall == null) {
					LeftWall ();
				}
			}
		}
	}
}
