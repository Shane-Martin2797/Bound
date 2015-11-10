using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class UIStartText : MonoBehaviour {

	public Text text;
	float timer = 3f; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;
		text.text = timer.ToString ("0");
		if (timer <= 1) {
			text.text = "Fight!"; 
		}

		if(timer <= 0)
		{
			text.text = ("");
		}
	
	}
}
