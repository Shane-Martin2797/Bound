using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : SingletonBehaviour<AudioController>
{
	private AudioSource audioSource;
	
	protected override void OnSingletonAwake ()
	{
		audioSource = GetComponent<AudioSource> ();	
	}
	
	public void PlayAudioOneShot (AudioClip audioClip)
	{
		audioSource.PlayOneShot (audioClip);
		//audioSource.Play ();
	}	
		
	protected override void OnSingletonDestroy ()
	{
		
	}
}