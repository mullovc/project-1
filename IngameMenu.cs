using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour {
	
	public Main 		main;
	
	public Camera		mapCamera_prefab;
	Camera				mapCamera;
	public GUISkin		buttonSkin;
	Control				control;
	
	float				mapZoom;
	Vector3				lastFrameMousePos;
	bool				showFPS;
	float				timeSinceLastFPSCheck;
	float				FPSCheckInterval = 5;
	int					frames;
	int					FPS;
	MenuRegistry		showMenuRegistry;
	Vector2				scrollPosition;
	
	
	enum MenuRegistry {
		None,
		Settings,
		Cheats,
		Map,
		Stats
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;
		if(showFPS)
			GUI.Label(new Rect(Screen.width - 50,10,50,50),FPS.ToString());
	}
	
	public void menuWindow(int windowIndex)
	{
		GUILayout.BeginArea(new Rect(20,20,Screen.width - 80,Screen.height - 40));
		
			GUILayout.BeginArea(new Rect(0,0,Screen.width - 80,Screen.height * 0.1f));
		
				GUILayout.BeginHorizontal();
			
				if(GUILayout.Button("Settings"))
				{
					showMenuRegistry = MenuRegistry.Settings;
				}
				if(GUILayout.Button("Cheats"))
				{
					showMenuRegistry = MenuRegistry.Cheats;
				}
				if(GUILayout.Button("Map"))
				{
					showMenuRegistry = MenuRegistry.Map;
					mapZoom = 35;
					
				}
				if(GUILayout.Button("Stats"))
				{
					showMenuRegistry = MenuRegistry.Stats;
				}
				if(GUILayout.Button("Close"))
				{
					if(mapCamera != null)
						Destroy(mapCamera.gameObject);
					main.menuIsOpen = false;
					main.gameIsPaused = false;
				}
				if(GUILayout.Button("Quit"))
				{
					Application.LoadLevel(0);
				}
				
				GUILayout.EndHorizontal();
			
			GUILayout.EndArea();
		
			if(mapCamera != null && showMenuRegistry != MenuRegistry.Map)
			{
				Destroy(mapCamera.gameObject);
			}
			
			switch(showMenuRegistry)
			{
				case MenuRegistry.Settings:
				
					GUILayout.BeginArea(new Rect(0,Screen.height * 0.11f,Screen.width / 2,Screen.height));
						
						if(GUILayout.Button("save",GUILayout.Height(Screen.height * 0.1f)))
							main.saveLoader.saveGame();
						main.control.alternativeControl = GUILayout.Toggle(main.control.alternativeControl,"Left screenhalf control");
						showFPS = GUILayout.Toggle(showFPS,"Show FPS");
					
					GUILayout.EndArea();
					break;
			
				case MenuRegistry.Cheats:
				
					scrollPosition = GUI.BeginScrollView(new Rect(0,Screen.height * 0.11f,Screen.width / 2 + 35,Screen.height - 120),scrollPosition,new Rect(0,0,Screen.width / 2,Screen.height * 2));
						
						main.godMode = GUILayout.Toggle(main.godMode,"Godmode");
						if(GUILayout.Button("Restore HP",GUILayout.Width(Screen.width * 0.2f)))
							main.stats.updateStats(true);
						GUILayout.Label("Character Level: " + main.stats.level);
						main.stats.level = (int)GUILayout.HorizontalSlider(main.stats.level,1,100,GUILayout.Width(Screen.width / 2));
			
						for(int i = 0; i < 2; i++)
						{
							GUILayout.Label("Spawner " + (i + 1) + " NPC-level: " + main.spawner[i].NPCLevel);
							main.spawner[i].NPCLevel = (int)GUILayout.HorizontalSlider(main.spawner[i].NPCLevel,1,100,GUILayout.Width(Screen.width / 2));
							GUILayout.Label("Spawner " + (i + 1) + " spawnrate: " + main.spawner[i].spawnRate);
							main.spawner[i].spawnRate = (int)GUILayout.HorizontalSlider(main.spawner[i].spawnRate,0,500,GUILayout.Width(Screen.width / 2));
							GUILayout.Label("Spawner " + (i + 1) + " spawnlimit: " + main.spawner[i].spawnLimit);
							main.spawner[i].spawnLimit = (int)GUILayout.HorizontalSlider(main.spawner[i].spawnLimit,0,100,GUILayout.Width(Screen.width / 2));
							GUILayout.Label("Spawner " + (i + 1) + " activity radius: " + main.spawner[i].activityRadius);
							main.spawner[i].activityRadius = (int)GUILayout.HorizontalSlider(main.spawner[i].activityRadius,0,50,GUILayout.Width(Screen.width / 2));
						}
					
					GUI.EndScrollView();
					break;
				
				case MenuRegistry.Map:
				
					if(mapCamera == null)
						mapCamera = Instantiate(mapCamera_prefab,mapCamera_prefab.transform.position,Quaternion.AngleAxis(90,Vector3.right)) as Camera;
					mapCamera.pixelRect = new Rect(40,40,Screen.width - 80,Screen.height - 100);
					mapZoom = GUI.HorizontalSlider(new Rect(0,100,Screen.width * 0.25f,25),mapZoom,2,50);
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
					break;
			}
		
		GUILayout.EndArea();
	}
	
	
	void Update ()
	{
		timeSinceLastFPSCheck += Time.deltaTime;
		frames++;
		
		if(timeSinceLastFPSCheck >= FPSCheckInterval)
		{
			FPS = (int)(frames / FPSCheckInterval);
			frames = 0;
			timeSinceLastFPSCheck = 0;
		}
	}
}