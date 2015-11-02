using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{
	public BallSpell damageBall;
	public Transform spawnPoint;
	PlayerController player;
	float castTime;
	
	public override void OnSpellAwake ()
	{
		player = GetComponent<PlayerController> ();
		castTime = player.castTime;
		castTime *= CastTimeModifier;
	}
	
	
	
	public override void HoldCast ()
	{
		if (castTime < 0) {
		
		}
	}
	public override void ReleaseCast ()
	{
		BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
		ball.SetDamage (20);
	}
}
