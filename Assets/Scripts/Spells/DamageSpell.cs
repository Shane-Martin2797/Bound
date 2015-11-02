using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{
	public BallSpell damageBall;
	public Transform spawnPoint;
	public float damage = 20f;
	
	
	public override void HoldCast ()
	{
		if(!onCooldown){
		if (!canCast) {
			castTime -= Time.deltaTime;
			if (castTime <= 0) {
				canCast = true;
			}
		}
		}
		if (canCast) {
			BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
			ball.SetDamage (damage);
			CooldownValues();
			ResetValues ();
		}
	}
	public override void ReleaseCast ()
	{
		if (canCast) {
			BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
			ball.SetDamage (damage);
			CooldownValues();
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
