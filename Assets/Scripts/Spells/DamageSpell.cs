using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{
	public BallSpell damageBall;
	public AoEBallSpell damageBallAoE;
	public Transform spawnPoint;
	public float damage = 20f;
	
	
	public override void HoldCast ()
	{
		if (!onCooldown) {
			if (!canCast) {
				castTime -= Time.deltaTime;
				if (castTime <= 0) {
					canCast = true;
				}
			}
		}
		if (canCast) {
			Shoot ();
		}
	}
	public override void ReleaseCast ()
	{
		if (canCast) {
			BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
			ball.SetDamage (damage);
			CooldownValues ();
		}
		ResetValues ();
	}
	
	void Shoot ()
	{
		if (damageBall != null) {
			BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
			ball.SetDamage (damage);
			CooldownValues ();
			ResetValues ();
		} else if (damageBallAoE != null) {
			AoEBallSpell ball = Instantiate (damageBallAoE, spawnPoint.transform.position, transform.rotation) as AoEBallSpell;
			ball.SetDamage (damage);
			CooldownValues ();
			ResetValues ();
		}
		
	}

	public override void ResetValues ()
	{
		castTime = player.castTime;
		CastTimeModifier *= CastTimeModifier;
		canCast = false;
	}

}
