using UnityEngine;
using System.Collections;

public class Ball : CollisionTriggerBehaviour
{
	public float damage;
	public float lifetime = 5f;
	public float speed = 5;
	public GameObject particles;

	public virtual void OnEnable()
	{
		if(particles != null)
		{
			particles.SetActive(true);
		}
	}

	public virtual void OnDisable()
	{
		if(particles != null)
		{
			particles.SetActive(false);
		}
	}

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
