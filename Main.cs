using UnityEngine;
using System.Collections;
using System.IO;

public class Main : MonoBehaviour {
	
	MyTerrain 			terrain;
	
	public GameObject 	terrain_prefab;
	public Transform	character_prefab;
	public Transform 	character;
	
	// Use this for initialization
	void Start ()
	{
		character = Instantiate(character_prefab,new Vector3(0,1,0),Quaternion.identity) as Transform;
		character.parent = this.transform;
		terrain = terrain_prefab.GetComponent<MyTerrain>();
		terrain.buidTerrain();
	}
	
	void startGame()
	{
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
