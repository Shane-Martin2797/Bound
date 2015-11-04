using UnityEngine;
using System.Collections;

public class AoEBallSpell : Ball
{
	public float radius = 3;

	public override void NotifyEnter (GameObject gameObj)
	{
		CleanUp ();
	}
	
	public override void CleanUp ()
	{
		RaycastHit2D[] hit = Physics2D.CircleCastAll (transform.position, radius, transform.up);
		for (int i = 0; i < hit.Length; ++i) {
			if (hit [i].collider != null) {
				PlayerController player = hit [i].collider.GetComponent<PlayerController> ();
				if (player != null) {
					player.DamagePlayer (damage);
				}
			}
		}
		Destroy (this.gameObject);
	}
}
