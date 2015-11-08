using UnityEngine;
using System.Collections;

public class AoEBallSpell : Ball
{
	public float radius = 5;
	public float minDamage = 5;

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
					float distance = Vector3.Distance (transform.position, player.transform.position);
					float normal = ((damage - minDamage) * ((radius - distance) / radius)) + minDamage;
					player.DamagePlayer (normal);
				}
			}
		}
		Destroy (this.gameObject);
	}
}
