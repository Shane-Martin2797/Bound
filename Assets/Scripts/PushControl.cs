using UnityEngine;
using System.Collections;

public class PushControl : MonoBehaviour
{
	private PlayerController player;
	
	void Awake ()
	{
		player = this.GetComponent<PlayerController> ();
	}
	
	void Update ()
	{
		if (player.heldWall != null) {
			if (player.inputDevice.LeftStickY.Value > 0 || player.inputDevice.LeftStickX.Value > 0) {
				PushWall ();
			}
		}
	}
	
	void PushWall ()
	{
		Vector3 movementVector = new Vector3 (0, 0, player.inputDevice.LeftStickY.Value);
		if (player.transform.localEulerAngles.y > 90 && player.transform.localEulerAngles.y < 270) {
			movementVector = -movementVector;
		}
		player.heldWall.transform.Translate (movementVector * Time.deltaTime * player.movementSpeed);
		Vector3 pos = player.heldWall.transform.position;
		pos.z = player.transform.position.z;
		pos.y = player.transform.position.y;
		player.transform.position = pos;
		
		
	}
	
}
