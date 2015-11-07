using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScore : MonoBehaviour
{

	public Text text;
	
	void Awake ()
	{
		//text.text = "Team " + GameController.winTeam + " Wins" + "\n" + " Score: " + PlayerPrefs.GetInt ("Team" + GameController.winTeam, 0);
	}
}
