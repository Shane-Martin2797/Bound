using UnityEngine;
using System.Collections;

public class DashSpell : Spell
{
	public float distance = 20;
	
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
			player.transform.Translate (Vector3.up * distance);
		}
		ResetValues ();
	}
	
	public override void ResetValues ()
	{
		castTime = player.castTime;
		castTime *= CastTimeModifier;
		canCast = false;
	}



}
