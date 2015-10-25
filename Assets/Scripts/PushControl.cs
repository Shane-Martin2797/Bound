using UnityEngine;
using System.Collections;

public class PushControl : MonoBehaviour
{
	private PlayerController player;
	
	private WallController heldWall;
	public WallController targetedWall;
	public LayerMask collidersHit;
	public float grabDist = 1;
	public float moveModifier = .7f;
	
	void Awake ()
	{
		player = this.GetComponent<PlayerController> ();
	}
	
	void Update ()
	{
		if (player.inputDevice != null) {
			if (player.inputDevice.Action1.IsPressed) {
				HoldWall ();
			}
		}
		if (heldWall != null) {
			PushWall ();
		}
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
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.forward, grabDist, collidersHit);
		Debug.DrawRay (transform.position, transform.forward, Color.blue);
		if (hit != null) {
			if (hit.collider != null) {
				WallController wall = hit.collider.GetComponent<WallController> ();
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
			heldWall = null;
			player.canMove = true;
		} else if (targetedWall != null && CheckWall ()) {
			heldWall = targetedWall;
			player.canMove = false;
		}
	}
}
