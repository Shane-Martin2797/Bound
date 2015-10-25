using UnityEngine;
using System.Collections;

public class GhostControl : MonoBehaviour
{
	private PlayerController player;
	public PlayerController heldPlayer;
	bool grabbed = false;
	bool inWall = false;
	float timer;
	float defaultTimer = .25f;
	float holdTimer = 3;
	float holdTimerDefault = 3;
	
	void OnCollisionEnter2D (Collision2D col)
	{
		PlayerController touchedPlayer = col.gameObject.GetComponent<PlayerController> ();
		if (col.gameObject.GetComponent<WallController> () != null) {
			TouchedWall ();
		} else if (touchedPlayer != null) {
			touchedPlayer.canMove = false;
			heldPlayer = touchedPlayer;
			GrabbedPlayer ();
			grabbed = true;
			holdTimer = holdTimerDefault;
		}
	}
	


	public void TouchedWall ()
	{
		GetComponent<Collider2D> ().enabled = false;
		inWall = true;
		timer = defaultTimer;
	}
	
	void GrabbedPlayer ()
	{
		heldPlayer.transform.parent = this.transform;
		heldPlayer.GetComponent<Collider2D> ().enabled = false;
	}
	
	void LetGoOfPlayer ()
	{
		grabbed = false;
		heldPlayer.transform.parent = null;
		heldPlayer.GetComponent<Collider2D> ().enabled = true;
		heldPlayer = null;
	}
	public void LeftWall ()
	{
		GetComponent<Collider2D> ().enabled = true;
	}
	
	
	void Awake ()
	{
		player = this.GetComponent<PlayerController> ();
	}
	// Use this for initialization
	void Start ()
	{
	
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
		
	}
}
