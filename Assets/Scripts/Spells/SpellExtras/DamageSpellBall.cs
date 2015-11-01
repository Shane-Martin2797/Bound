using UnityEngine;
using System.Collections;

public class DamageSpellBall : CollisionTriggerBehaviour
{
	public float damage = 20;
	float lifetime = 5f;

	void Update ()
	{
		transform.Translate (Vector3.up * Time.deltaTime);
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			CleanUp ();
		}
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
