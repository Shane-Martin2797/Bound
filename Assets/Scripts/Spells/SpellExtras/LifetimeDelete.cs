using UnityEngine;
using System.Collections;

public class LifetimeDelete : MonoBehaviour
{

	public float lifetime = 3;
	
	// Update is called once per frame
	void Update ()
	{
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			CleanUp ();
		}
	}
	void CleanUp ()
	{
		Destroy (this.gameObject);
	}
}
