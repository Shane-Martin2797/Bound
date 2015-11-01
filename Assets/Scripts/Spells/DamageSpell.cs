using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{

	public DamageSpellBall damageBall;
	public Transform spawnPoint;
	
	public override void PressCast ()
	{
		DamageSpellBall ball = Instantiate (damageBall) as DamageSpellBall;
		ball.transform.position = spawnPoint.position;
	}
}
