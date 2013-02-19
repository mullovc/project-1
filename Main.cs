using UnityEngine;
using System.Collections;
using System.IO;

public class Main : MonoBehaviour {
	
	//public MyTerrain	terrain_prefab;
	//MyTerrain 			terrain;
	//public NPCSpawner	spawner_prefab;
	//public NPCSpawner	spawner;
	public NPCSpawner[]	spawner = new NPCSpawner[5];
	public GUISkin		buttonSkin;
	public Control		control;
	IngameMenu			ingameMenu;
	Stats				stats;
	
	public GameObject	character_prefab;
	public GameObject	character;
	
	public bool			gameIsPaused;
	public bool			gameOver = false;
	public bool			menuIsOpen = false;

	
	// Use this for initialization
	void Start ()
	{
		gameOver = false;
		character = Instantiate(character_prefab,new Vector3(0,1,0),Quaternion.identity) as GameObject;
		character.transform.parent = this.transform;
		control = character.GetComponent<Control>();
		control.main = this;
		ingameMenu = GetComponent<IngameMenu>();
		ingameMenu.main = this;
		stats = character.GetComponent<Stats>();
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;
		if(!menuIsOpen)
		{
			if(GUI.Button(new Rect(10,10,Screen.width * 0.1f,Screen.height * 0.1f),stats.HP.ToString()))
			{
				menuIsOpen = true;
				gameIsPaused = true;
			}
		}
		if(menuIsOpen)
		{
			GUI.Window(0,new Rect(20,20,Screen.width - 40, Screen.height - 40),ingameMenu.menuWindow,"Menu");
		}
		if(gameOver)
		{
			gameIsPaused = true;
			GUI.Box(new Rect(Screen.width / 4,Screen.height / 3,Screen.width / 2,Screen.height / 5),"Game Over");
		}
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
		if(stats.HP <= 0)
		{
			gameOver = true;
			stats.HP = 0;
		}
	}
}
