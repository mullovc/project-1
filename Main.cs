using UnityEngine;
using System.Collections;
using System.IO;

public class Main : MonoBehaviour {
	
	public MyTerrain	terrain_prefab;
	MyTerrain 			terrain;
	public NPCSpawner	spawner_prefab;
	NPCSpawner			spawner;
	public GUISkin		buttonSkin;
	public Camera		mapCamera_prefab;
	Camera				mapCamera;
	
	public GameObject	character_prefab;
	public GameObject	character;
	
	public bool			gameIsPaused;
	public bool			gameOver;
	int					showMenuRegister = 0;
	bool 				menuIsOpen = false;

	
	// Use this for initialization
	void Start ()
	{
		character = Instantiate(character_prefab,new Vector3(0,1,0),Quaternion.identity) as GameObject;
		character.transform.parent = this.transform;
		Control control = character.GetComponent<Control>();
		control.main = this;
		terrain = Instantiate(terrain_prefab,transform.position,Quaternion.identity)as MyTerrain;
		terrain.buidTerrain();
		spawner = Instantiate(spawner_prefab,transform.position,Quaternion.identity)as NPCSpawner;
		spawner.main = this;
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;
		if(!menuIsOpen)
		{
			if(GUI.Button(new Rect(10,10,60,50),"Avatar"))
			{
				menuIsOpen = true;
				gameIsPaused = true;
			}
		}
		if(menuIsOpen)
		{
			GUI.Window(0,new Rect(20,20,Screen.width - 40, Screen.height - 40),menuWindow,"Menu");
		}
		
	}
	
	void menuWindow(int windowIndex)
	{
		
		GUI.BeginGroup(new Rect(20,20,Screen.width - 40,Screen.height - 40));
		{
			if(GUI.Button(new Rect(0,0,70,20),"Settings"))
			{
				showMenuRegister = 1;
			}
			if(GUI.Button(new Rect(70,0,70,20),"Map"))
			{
				showMenuRegister = 2;
			}
			if(GUI.Button(new Rect(140,0,70,20),"Stats"))
			{
				showMenuRegister = 3;
			}
			if(GUI.Button(new Rect(210,0,70,20),"Close"))
			{
				showMenuRegister = 0;
				menuIsOpen = false;
				gameIsPaused = false;
			}
			if(GUI.Button(new Rect(280,0,70,20),"Quit"))
			{
				Application.LoadLevel(0);
			}
			if(mapCamera != null && showMenuRegister != 2)
			{
				Destroy(mapCamera.gameObject);
				if(showMenuRegister != 0)
					gameIsPaused = true;
			}
			if(showMenuRegister == 1)
			{
				//alternativeControl = GUI.Toggle(new Rect(0,30,150,20),alternativeControl,"Left screenhalf control");
			}
			if(showMenuRegister == 2)
			{
				if(mapCamera == null)
					mapCamera = Instantiate(mapCamera_prefab,mapCamera_prefab.transform.position,Quaternion.AngleAxis(90,Vector3.right)) as Camera;
				mapCamera.pixelRect = new Rect(40,40,Screen.width - 80,Screen.height - 100);
				gameIsPaused = false;
			}
		}
		GUI.EndGroup();
	}
	
	void loadGame()
	{
		string x_str = "", z_str = "";
		int x = 0, z = 0;

        FileInfo SourceFile = new FileInfo ("save.txt");
        StreamReader reader = SourceFile.OpenText();
		
        x_str = reader.ReadLine();
        z_str = reader.ReadLine();
		reader.Close();
		
		x = int.Parse(x_str);
		z = int.Parse(z_str);
		
		transform.position = new Vector3(x,6.622f,z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
