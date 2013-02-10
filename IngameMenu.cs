using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour {
	
	public Main main;
	
	public Camera		mapCamera_prefab;
	Camera				mapCamera;
	public GUISkin		buttonSkin;
	Control				control;
	NPCSpawner			spawner;
	
	int					showMenuRegister = 0;
	float				mapZoom;
	Vector3				lastFrameMousePos;
	
	
	// Use this for initialization
	void Start ()
	{
	}
	void OnGUI()
	{
		GUI.skin = buttonSkin;
	}
	
	public void menuWindow(int windowIndex)
	{
		GUILayout.BeginArea(new Rect(20,20,Screen.width - 80,Screen.height - 40));
		
			GUILayout.BeginArea(new Rect(0,0,Screen.width - 80,Screen.height * 0.1f));
		
				GUILayout.BeginHorizontal();
			
				if(GUILayout.Button("Settings"))
				{
					showMenuRegister = 1;
				}
				if(GUILayout.Button("Map"))
				{
					showMenuRegister = 2;
					mapZoom = 35;
					
				}
				if(GUILayout.Button("Stats"))
				{
					showMenuRegister = 3;
				}
				if(GUILayout.Button("Close"))
				{
					showMenuRegister = 0;
					main.menuIsOpen = false;
					main.gameIsPaused = false;
				}
				if(GUILayout.Button("Quit"))
				{
					Application.LoadLevel(0);
				}
				
				GUILayout.EndHorizontal();
			
			GUILayout.EndArea();
		
			if(mapCamera != null && showMenuRegister != 2)
			{
				Destroy(mapCamera.gameObject);
			}
			if(showMenuRegister == 1)
			{
				GUILayout.BeginArea(new Rect(0,Screen.height * 0.11f,Screen.width / 2,Screen.height));
			
					main.control.alternativeControl = GUILayout.Toggle(main.control.alternativeControl,"Left screenhalf control");
					GUILayout.Label("enemy spawnrate: " + main.spawner.spawnRate);
					main.spawner.spawnRate = (int)GUILayout.HorizontalSlider(main.spawner.spawnRate,0,500);
					GUILayout.Label("enemy spawnlimit: " + main.spawner.spawnLimit);
					main.spawner.spawnLimit = (int)GUILayout.HorizontalSlider(main.spawner.spawnLimit,0,100);
			
				GUILayout.EndArea();
			}
			if(showMenuRegister == 2)
			{
				if(mapCamera == null)
					mapCamera = Instantiate(mapCamera_prefab,mapCamera_prefab.transform.position,Quaternion.AngleAxis(90,Vector3.right)) as Camera;
				mapCamera.pixelRect = new Rect(40,40,Screen.width - 80,Screen.height - 100);
				mapZoom = GUI.HorizontalSlider(new Rect(0,100,Screen.width * 0.25f,25),mapZoom,5,50);
				mapCamera.orthographicSize = mapZoom;
				
				if(Input.GetMouseButtonDown(0))
				{
					lastFrameMousePos = Input.mousePosition;
				}
				if(Input.GetMouseButton(0))
				{
					float deltaX = (lastFrameMousePos.x - Input.mousePosition.x) * 0.005f * mapZoom;
					float deltaY = (lastFrameMousePos.y - Input.mousePosition.y) * 0.005f * mapZoom;
					mapCamera.transform.Translate(deltaX,deltaY,0);
					lastFrameMousePos = Input.mousePosition;
				}
			}
		
		GUILayout.EndArea();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
