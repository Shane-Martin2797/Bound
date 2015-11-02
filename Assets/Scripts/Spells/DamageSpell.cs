using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{

	public BallSpell damageBall;
	public Transform spawnPoint;
	
	public override void PressCast ()
	{
		BallSpell ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as BallSpell;
		ball.SetDamage (20);
	}
}
