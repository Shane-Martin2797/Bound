using UnityEngine;
using System.Collections;

public class AnimationControllerTG : MonoBehaviour
{

	public Transform upperBody; 
	public PlayerController player;
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
	void Update ()
	{

		if (player != null) {
			if (player.inputDevice != null) {
				if (player.inputDevice.LeftBumper.IsPressed || player.inputDevice.LeftTrigger.IsPressed
					|| player.inputDevice.RightBumper.IsPressed || player.inputDevice.RightTrigger.IsPressed) {
					animation.Blend ("TGCast");
					isShooting = true;
				}
				if (player.inputDevice.LeftBumper.WasReleased || player.inputDevice.LeftTrigger.WasReleased
					|| player.inputDevice.RightBumper.WasReleased || player.inputDevice.RightTrigger.WasReleased) {
					animation.Stop ("TGCast");
					isShooting = false; 
				}
				//Movement parameters set 
				float angle = player.transform.localEulerAngles.z;
				float difference = 180 - (angle % 180);
				float normalX = 0;
				float normalY = 0;
				if (difference < 135 && difference > 45) {
					normalY = player.inputDevice.LeftStickX.Value;
					normalX = player.inputDevice.LeftStickY.Value;
				} else {
					normalY = player.inputDevice.LeftStickY.Value;
					normalX = player.inputDevice.LeftStickX.Value;
				}
				if (angle < 225 && angle > 45) {
					normalY = -normalY;
				}
				
				if (normalY > 0) {
					isForward = true;
					animation.Play ("TGRun");
			
				}
				if (normalY == 0 && normalX == 0) {
					isForward = false;
					animation.Play ("TGIdle");
			
				}
				if (normalY < 0) {
					isBackwards = true; 
					animation.Play ("TGBack");
				}
				if (normalY == 0) {
					isBackwards = false; 
				}
		
				if (normalX != 0) {
					isSideways = true;
					if (!isForward && !isBackwards) {
						animation.Play ("TGStrafe");
					}
				}
		
				if (normalX == 0) {
					isSideways = false; 
				}
		
				if (isForward && isSideways) {
					animation.Play ("TGRun");
				}
		
				if (isBackwards && isSideways) {
					animation.Play ("TGBack");
				}
			}
		}
	}
}

