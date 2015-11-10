using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
	public Transform upperBody; 
	public PlayerController player;
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
		if (player != null) {
			if (player.inputDevice != null) {
				if (player.inputDevice.LeftBumper.IsPressed || player.inputDevice.LeftTrigger.IsPressed
					|| player.inputDevice.RightBumper.IsPressed || player.inputDevice.RightTrigger.IsPressed) {
					animation.Blend ("GhostTopCast");
					isShooting = true;
				}
				if (player.inputDevice.LeftBumper.WasReleased || player.inputDevice.LeftTrigger.WasReleased
					|| player.inputDevice.RightBumper.WasReleased || player.inputDevice.RightTrigger.WasReleased) {
					animation.Stop ("GhostTopCast");
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
					animation.Play ("RunGhost");

				}
				if (normalY == 0 && normalX == 0) {
					isForward = false; 
					animation.Play ("GhostIdle");
				}
				if (normalY < 0) {
					isBackwards = true; 
					animation.Play ("BackpedalGhost");
				}
				if (normalY == 0) {
					isBackwards = false; 
				}

				if (normalX != 0) {
					isSideways = true;
					if (!isForward && !isBackwards) {
						animation.Play ("GhostStrafe");
					}
				}

				if (normalX == 0) {
					isSideways = false; 
				}

				if (isForward && isSideways) {
					animation.Play ("RunGhost");
				}

				if (isBackwards && isSideways) {
					animation.Play ("BackpedalGhost");
				}
			}
		}
	}
}
