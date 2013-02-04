using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {					//dieser Script kommt nur in der Szene 0 vor und erstellt die GUI fuer das Hauptmenu
	
	public GUISkin ButtonSkin;
	
	void Start () {
	
	}
	
	void OnGUI()
	{
		GUI.skin = ButtonSkin;
		GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));
		{
			if(GUI.Button(new Rect(0,0,100,20),"New Game"))
			{
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(0,25,100,20),"Load"))
			{
														//wird noch hinzugefuegt
			}
			if(GUI.Button(new Rect(0,50,100,20),"Settings"))
			{
														//wird noch hinzugefuegt
			}
			if(GUI.Button(new Rect(0,75,100,20),"Quit"))
			{
				Application.Quit();
			}
		}
		GUI.EndGroup();
	}
	
	void Update () {
	
	}
}
