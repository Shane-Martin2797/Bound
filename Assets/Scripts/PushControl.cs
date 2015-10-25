using UnityEngine;
using System.Collections;

public class PushControl : MonoBehaviour
{
	private PlayerController player;
	
	private WallController heldWall;
	public WallController targetedWall;
	public LayerMask collidersHit;
	
	void Awake ()
	{
		player = this.GetComponent<PlayerController> ();
	}
	
	void Update ()
	{
		CheckWall ();
	}
	
	void PushWall ()
	{
	
	}
	void CheckWall ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.up, 1, collidersHit);
		Debug.DrawRay (transform.position, transform.forward, Color.blue);
		if (hit != null) {
			if (hit.collider != null) {
				WallController wall = hit.collider.GetComponent<WallController> ();
				if (wall != null) {
					targetedWall = wall;
				}
			}
		}
	}
}
