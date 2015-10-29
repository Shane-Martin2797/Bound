using UnityEngine;
using System.Collections;

public class PushControl : MonoBehaviour
{
	public PlayerController player;
	
	public AudioClip footstepSound;
	
	public PushWallController heldWall;
	public PushWallController targetedWall;
	public float grabDist = 1;
	public float moveModifier = .7f;
	
	public float visionDistance = 100;
	public float visionCone = 30;
	public LayerMask collidersHit;

	
	
	void Start ()
	{
		player = this.GetComponent<PlayerController> ();
		GameController.Instance.HumanList.Add (this);
	}
	
	void Update ()
	{
		if (player.inputDevice != null) {
			if (player.inputDevice.Action1.WasPressed) {
				HoldWall ();
			}
		}
		if (heldWall != null) {
			PushWall ();
		}
		SearchForEnemyGhost ();
	}
	
	void PushWall ()
	{
		if (player.inputDevice.LeftStickX.Value != 0 || player.inputDevice.LeftStickY.Value != 0) {
			Vector3 pos = heldWall.transform.position - transform.position;
			heldWall.Move (new Vector2 (player.inputDevice.LeftStickX.Value, player.inputDevice.LeftStickY.Value), (player.movementSpeed * moveModifier));
			Vector3 pos2 = heldWall.transform.position - transform.position;
			Vector3 posNow = transform.position - (pos - pos2);
			transform.position = posNow;
		}
	}
	
	bool CheckWall ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.forward, grabDist, collidersHit.value);
		Debug.DrawRay (transform.position, transform.forward, Color.blue);
		if (hit != null) {
			if (hit.collider != null) {
				PushWallController wall = hit.collider.GetComponent<PushWallController> ();
				if (targetedWall != null) {
					if (wall == targetedWall) {
						return true;
					}
				}
			}
		}
		return false;
	}
	
	void HoldWall ()
	{
		if (heldWall != null) {
			DropWall ();
		} else if (targetedWall != null && CheckWall ()) {
			heldWall = targetedWall;
			player.canMove = false;
		}
	}
	
	public void DropWall ()
	{
		heldWall = null;
		player.canMove = true;
	}
	
	void SearchForEnemyGhost ()
	{
		for (int i = 0; i < GameController.Instance.GhostList.Count; ++i) {
			GhostControl ghost = GameController.Instance.GhostList [i];
			if (ghost.player.team != player.team) {
				Vector3 deltaToPlayer = (ghost.transform.position - transform.position);
				Vector3 directionToPlayer = deltaToPlayer.normalized;
			
				float dot = Vector3.Dot (transform.forward, directionToPlayer);
				float cone = Mathf.Cos (visionCone / 2 * Mathf.Deg2Rad);
				Debug.DrawRay (transform.position, directionToPlayer, Color.red);
			
				if (dot > cone) {
					float distance = Vector3.Distance (transform.position, ghost.transform.position);
					if (distance < visionDistance) {
						Physics2D.queriesHitTriggers = false;
						RaycastHit2D hit = Physics2D.Raycast (transform.position + transform.up, directionToPlayer, visionDistance, collidersHit.value);
						if (hit != null) {
							if (ghost != null) {
								if (hit.collider != null) {
									if (hit.collider.gameObject == ghost.gameObject) {
										if (hit.collider.gameObject.GetComponent<PlayerController> () != null) {
											if (hit.collider.gameObject.GetComponent<PlayerController> ().team != player.team) {
												if (hit.collider != null) {
													ghost.Stun ();
												}
											}
										}
									}
								}
							}
						}
				
					}
				}
			}
		}
	}
}