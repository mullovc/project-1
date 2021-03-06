using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GUISkin ButtonSkin;
	
	void Start ()
	{
	}
	
	void OnGUI()
	{
		GUI.skin = ButtonSkin;
		GUILayout.BeginArea(new Rect(Screen.width / 3, Screen.height / 4, Screen.width / 3, Screen.height / 2));
		{
			if(GUILayout.Button("New Game"))
			{
				Application.LoadLevel(1);
			}
			if(GUILayout.Button("Load"))
			{
				GameObject load = new GameObject();
				load = Instantiate(load) as GameObject;
				load.gameObject.name = "load";
				DontDestroyOnLoad(load);
				Application.LoadLevel(1);
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
