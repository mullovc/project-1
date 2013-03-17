using UnityEngine;
using System.Collections;
using System.IO;

public class Main : MonoBehaviour {
	
	public NPCSpawner[]	spawner = new NPCSpawner[5];
	public GUISkin		buttonSkin;
	public Control		control;
	IngameMenu			ingameMenu;
	public Stats		stats;
	public SaveLoader			saveLoader;
	
	public GameObject	character_prefab;
	public GameObject	character;
	
	public bool			gameIsPaused;
	public bool			gameOver;
	public bool			menuIsOpen = false;
	public bool			godMode;

	
	// Use this for initialization
	void Start ()
	{
		character = Instantiate(character_prefab,new Vector3(0,1,0),Quaternion.identity) as GameObject;
		character.transform.parent = this.transform;
		control = character.GetComponent<Control>();
		control.main = this;
		ingameMenu = GetComponent<IngameMenu>();
		ingameMenu.main = this;
		stats = character.GetComponent<Stats>();
		stats.statsOwner = this.transform;
		stats.level = 1;
		stats.updateStats(true);
		saveLoader = GetComponent<SaveLoader>();
		saveLoader.main = this;
		
		if(GameObject.Find("load") != null)
		{
			saveLoader.loadGame();
			Destroy(GameObject.Find("load"));
		}
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;
		if(!menuIsOpen)
		{
			if(GUI.Button(new Rect(Screen.width * 0.83f,10,Screen.width * 0.15f,Screen.height * 0.15f),"Menu"))
			{
					menuIsOpen = true;
					gameIsPaused = true;
			}
			GUI.Box(new Rect(10,10,(float)stats.HP / (float)stats.maxHP * 200,Screen.height * 0.07f),stats.HP.ToString());
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
	
	
	void Update ()
	{
		if(stats.HP <= 0 && stats.maxHP > 0)
		{
			gameOver = true;
			stats.HP = 0;
		}
	}
}
