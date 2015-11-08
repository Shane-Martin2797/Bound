using UnityEngine;
using System.Collections;

public class AoEIndicator : MonoBehaviour
{

	public AoEBallSpell ball;

	// Use this for initialization
	void Start ()
	{
		float num = ball.radius / 10;
		if (ball != null) {
			this.transform.localScale = new Vector3 (num, num, 1);
		}
	
	}
}