using UnityEngine;
using System.Collections;

public class AnimationControllerTG : MonoBehaviour {

	public Transform upperBody; 
	Animation animation;

	bool isForward; 
	bool isSideways;
	bool isBackwards;
	bool isShooting;

	// Use this for initialization
	void Start ()
	{
		animation = GetComponent<Animation> ();
		animation ["TGCast"].layer = 2;
		animation ["TGCast"].AddMixingTransform (upperBody);

		animation.Play ("TGIdle");

		animation ["TGIdle"].speed = 1.5f;
		animation ["TGRun"].speed = 2f;
		animation ["TGBack"].speed = 2.5f;
		animation ["TGStrafe"].speed = 2f;
		animation ["TGCast"].speed = 1f;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.O) || Input.GetKey (KeyCode.P)) 
		{
			animation.Blend("TGCast");
			isShooting = true;
		}
		if (Input.GetKeyUp (KeyCode.O))
		{
			animation.Stop("TGCast");
			isShooting = false; 
		}
		//Movement parameters set 
		if (Input.GetKey (KeyCode.W)) 
		{
			isForward = true;
			animation.Play ("TGRun");
			
		}
		if (Input.GetKeyUp (KeyCode.W)) 
		{
			isForward  = false; 
			animation.Play("TGIdle");
			
		}
		if (Input.GetKey(KeyCode.S)) 
		{
			isBackwards = true; 
			animation.Play ("TGBack");
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
				animation.Play ("TGStrafe");
			}
		}
		
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) 
		{
			isSideways = false; 
		}
		
		if (isForward && isSideways) 
		{
			animation.Play("TGRun");
		}
		
		if (isBackwards && isSideways)
		{
			animation.Play ("TGBack");
		}
	}
}

