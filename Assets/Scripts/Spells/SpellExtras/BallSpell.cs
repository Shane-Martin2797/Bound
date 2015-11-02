using UnityEngine;
using System.Collections;

public class BallSpell : CollisionTriggerBehaviour
{
	float damage = 20;
	float lifetime = 5f;
	public float speed = 5;

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
		Destroy (this.gameObject);
	}
}
