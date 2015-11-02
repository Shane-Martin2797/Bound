using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{
	public BallSpell damageBall;
	public Transform spawnPoint;
	PlayerController player;
	public float damage = 20f;
	
	public override void OnSpellAwake ()
	{
		player = transform.parent.GetComponent<PlayerController> ();
		castTime = player.castTime;
		castTime *= CastTimeModifier;
	}
	
	
	
	public override void HoldCast ()
	{
		if (!canCast) {
			castTime -= Time.deltaTime;
			if (castTime <= 0) {
				canCast = true;
			}
		}
	}
	public override void ReleaseCast ()
	{
		if (canCast) {
			BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
			ball.SetDamage (damage);
		}
		ResetValues ();
	}


	public override void ResetValues ()
	{
		castTime = player.castTime;
		CastTimeModifier *= CastTimeModifier;
		canCast = false;
	}

}
