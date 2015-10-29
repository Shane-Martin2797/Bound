using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BGMController : SingletonBehaviour<BGMController>
{
	private AudioSource audioSource;
	public List<AudioClip> BGMs = new List<AudioClip> ();
	
	
	
	
	protected override void OnSingletonAwake ()
	{
		audioSource = GetComponent<AudioSource> ();
	}
	
	public void ChangeMusic (int index)
	{
		int i = index % BGMs.Count;
		
		audioSource.Stop ();
		
		audioSource.clip = BGMs [i];
		
		audioSource.loop = true;
		
		audioSource.Play ();
	}
}
