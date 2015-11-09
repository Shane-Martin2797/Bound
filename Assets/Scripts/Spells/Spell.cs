using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{
	public float CastTimeModifier = 1f;
	public float castTime;
	public bool canCast;
	public PlayerController player;
	public bool onCooldown;
	public float cooldownTime;
	public float cooldownTimer;
	
		
	public virtual void Awake ()
	{
		player = transform.parent.GetComponent<PlayerController> ();
		ResetValues ();
		OnSpellAwake ();
	}
	public virtual void Update ()
	{
		if (onCooldown) {
			cooldownTimer -= Time.deltaTime;
			if (cooldownTimer < 0) {
				onCooldown = false;
			}
		}
		OnSpellUpdate ();
	}
	public virtual void CooldownValues ()
	{
		cooldownTimer = cooldownTime;
		onCooldown = true;
	}
	
	//This occurs on button press... This is used for immediate casting of spells
	public virtual void PressCast ()
	{
	
	}
	
	//This occurs as the button is held down... this can be used in conjunction with 
	//Release and a Timer to used charged spells
	public virtual void HoldCast ()
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
		if (canCast) {
			ReleaseCast();
		}
	}
	
	//This occurs as the button is released... this can be used to activate a spell that required a 
	//charge and will be used to cast the spell upon release of the button.
	public virtual void ReleaseCast ()
	{
		Shoot ();
	}
	
	public virtual void OnSpellAwake ()
	{
	
	}
	public virtual void OnSpellUpdate ()
	{
		
	}
	
	public virtual void Shoot ()
	{
	
	}
	public virtual void Charge ()
	{
	
	}
	
	public abstract void ResetValues ();
}
