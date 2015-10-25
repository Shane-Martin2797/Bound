using UnityEngine;
using System.Collections;

public class GhostControl : MonoBehaviour
{
	bool inWall = false;
	float timer;
	float defaultTimer = .75f;


	public void TouchedWall ()
	{
		GetComponent<Collider2D> ().enabled = false;
		inWall = true;
		timer = defaultTimer;
	}
	
	public void LeftWall ()
	{
		GetComponent<Collider2D> ().enabled = true;
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (inWall) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				inWall = false;
				LeftWall ();
			}
		}
	
	}
}
