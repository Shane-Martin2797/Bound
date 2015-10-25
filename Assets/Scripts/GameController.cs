using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>
{
	public static event System.Action<float> OnTimeChange;
	public List<GhostControl> GhostList = new List<GhostControl> ();
	
	protected override void OnSingletonAwake ()
	{
		
	}
}
