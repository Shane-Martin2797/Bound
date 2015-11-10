using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFader : MonoBehaviour {

	public Image panel;
	float fadeTimer = 5f;
	Color colorFader; 
	// Use this for initialization
	void Start () 
	{
		colorFader = new Color (1f, 1f, 1f, 0f);
		panel.CrossFadeColor (colorFader, fadeTimer, true, true); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
