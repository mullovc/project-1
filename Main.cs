using UnityEngine;
using System.Collections;
using System.IO;

public class Main : MonoBehaviour {							//diese Klasse ist am obersten in der Hierarchie und ist an der Main-Camera angeheftet
	
	MyTerrain 			terrain;
	
	public GameObject 	terrain_prefab;						//das Prefab, an dem der MyTerrain-Script angeheftet wird
	public Transform	character_prefab;					//das Prefab des Character-Assets
	public Transform 	character;							//die Instanz des Characters
	
	// Use this for initialization
	void Start ()
	{
		character = Instantiate(character_prefab,new Vector3(0,1,0),Quaternion.identity) as Transform;	//Character wird instanziiert
		character.parent = this.transform;																//Character bekommt die Main Camera als Parent
		terrain = terrain_prefab.GetComponent<MyTerrain>();												//ich hab im drecks C# noch keine andere Methode entdeckt, Objekte von MonoBehaviour-Klassen zu deklarieren
		terrain.buidTerrain();
	}
	
	void startGame()
	{
	}
	
	void loadGame()																						//ignorier diese Methode
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
