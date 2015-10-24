using UnityEngine;
using System.Collections;

public class PushControl : MonoBehaviour
{
	private PlayerController player;
	
	private WallController heldWall;
	public WallController targetedWall;
	
	void Awake ()
	{
		player = this.GetComponent<PlayerController> ();
	}
	
	void Update ()
	{
	}
	
	void PushWall ()
	{		
	}
	
}
