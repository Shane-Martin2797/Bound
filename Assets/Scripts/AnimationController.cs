using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
	public Transform upperBody; 
	Animation animation;

	bool isForward; 
	bool isSideways;
	bool isBackwards;
	bool isShooting;

	public float speed = 2f;

	// Use this for initialization
	void Start ()
	{
		animation = GetComponent<Animation> ();
		animation ["GhostTopCast"].layer = 2;
		animation ["GhostTopCast"].AddMixingTransform (upperBody);

		animation.Play ("GhostIdle"); 

		animation ["GhostIdle"].speed = 1.5f;
		animation ["RunGhost"].speed = 2f;
		animation ["BackpedalGhost"].speed = 2.5f;
		animation ["GhostStrafe"].speed = 2f;
		animation ["GhostTopCast"].speed = 1.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.O) || Input.GetKey (KeyCode.P)) 
		{
			animation.Blend("GhostTopCast");
			isShooting = true;
		}
		if (Input.GetKeyUp (KeyCode.O))
		{
			animation.Stop("GhostTopCast");
			isShooting = false; 
		}
		//Movement parameters set 
		if (Input.GetKey (KeyCode.W)) 
		{
			isForward = true;
			animation.Play ("RunGhost");

		}
		if (Input.GetKeyUp (KeyCode.W)) 
		{
			isForward  = false; 
			animation.Play("GhostIdle");

		}
		if (Input.GetKey(KeyCode.S)) 
		{
			isBackwards = true; 
			animation.Play ("BackpedalGhost");
		}
		if (Input.GetKeyUp (KeyCode.S))
		{
			isBackwards = false; 
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) 
		{
			isSideways = true;
			if(!isForward && !isBackwards)
			{
				animation.Play ("GhostStrafe");
			}
		}

		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) 
		{
			isSideways = false; 
		}

		if (isForward && isSideways) 
		{
			animation.Play("RunGhost");
		}

		if (isBackwards && isSideways)
		{
			animation.Play ("BackpedalGhost");
		}
	}
}
