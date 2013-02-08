using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GUISkin ButtonSkin;
	
	void Start () {
	
	}
	
	void OnGUI()
	{
		GUI.skin = ButtonSkin;
		GUILayout.BeginArea(new Rect(Screen.width / 3, Screen.height / 2 - 50, Screen.width / 3, Screen.height * 0.5f));
		{
			if(GUILayout.Button("New Game"))
			{
				Application.LoadLevel(1);
			}
			if(GUILayout.Button("Load"))
			{
			}
			if(GUILayout.Button("Settings"))
			{
			}
			if(GUILayout.Button("Quit"))
			{
				Application.Quit();
			}
		}
		GUILayout.EndArea();
	}
	
	void Update () {
	
	}
}
