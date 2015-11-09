using UnityEngine;
using System.Collections;

public class DashSpell : Spell
{

	public float distance = 20;
	public GameObject arrowIndicator;
	
	public override void HoldCast ()
	{
		if (!onCooldown) {
			if (!canCast) {
				castTime -= Time.deltaTime;
				Charge ();
				if (castTime <= 0) {
					canCast = true;
				}
			}
		}
		if(canCast)
		{
			ReleaseCast();
		}
	}
	
	public override void Charge ()
	{
		if (arrowIndicator != null) {
			Color colour = arrowIndicator.GetComponent<SpriteRenderer> ().color;
			if (castTime > 0) {
				colour.a += (Time.deltaTime / castTime);
			} else {
				colour.a = 1;
			}
			arrowIndicator.GetComponent<SpriteRenderer> ().color = colour;
		}
	}
	public override void ReleaseCast ()
	{
		if (canCast) {
			player.DashSetup ();
			CooldownValues ();
		}
		ResetColour ();
		ResetValues ();
	}
	
	void ResetColour ()
	{
		if (arrowIndicator != null) {
			Color colour = arrowIndicator.GetComponent<SpriteRenderer> ().color;
			colour.a = 0;
			arrowIndicator.GetComponent<SpriteRenderer> ().color = colour;
		}
	}
	
	public override void ResetValues ()
	{
		castTime = player.castTime;
		castTime *= CastTimeModifier;
		canCast = false;
	}
	

}
