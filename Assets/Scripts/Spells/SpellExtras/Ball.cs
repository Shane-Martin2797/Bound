using UnityEngine;
using System.Collections;

public class Ball : CollisionTriggerBehaviour
{
	public float damage = 20;
	public float lifetime = 5f;
	public float speed = 5;

	public virtual void Update ()
	{
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			CleanUp ();
		}
	}
	
	public virtual void SetDamage (float amount)
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
	
	public virtual void CleanUp ()
	{
		Destroy (this.gameObject);
	}
}
