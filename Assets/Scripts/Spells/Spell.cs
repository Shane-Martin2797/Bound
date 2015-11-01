using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{

	void Awake ()
	{
		OnSpellAwake ();
	}
	
	//This occurs on button press... This is used for immediate casting of spells
	public virtual void PressCast ()
	{
	
	}
	
	//This occurs as the button is held down... this can be used in conjunction with 
	//Release and a Timer to used charged spells
	public virtual void HoldCast ()
	{
	
	}
	
	//This occurs as the button is released... this can be used to activate a spell that required a 
	//charge and will be used to cast the spell upon release of the button.
	public virtual void ReleaseCast ()
	{
	
	}
	
	public virtual void OnSpellAwake ()
	{
	
	}
}
