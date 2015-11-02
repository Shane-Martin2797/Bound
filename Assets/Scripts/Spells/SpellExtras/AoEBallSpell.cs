using UnityEngine;
using System.Collections;

public class AoEBallSpell : CollisionTriggerBehaviour
{
	float damage = 20;
	float lifetime = 5f;
	public float speed = 5;
	public float radius = 3;

	void Update ()
	{
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			CleanUp ();
		}
	}
	public void SetDamage (float amount)
	{
		damage = amount;
	}
	public override void NotifyEnter (GameObject gameObj)
	{
		PlayerController player = gameObj.GetComponent<PlayerController> ();
		if (player != null) {
			player.DamagePlayer (damage);
		}
		CleanUp ();
	}
	
	void CleanUp ()
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
