using UnityEngine;
using System.Collections;
using System.IO;

public class SaveLoader : MonoBehaviour {
	
	public Main main;
	
	public void loadGame()
	{
		main.transform.position = new Vector3(load(1),13,load(2));
		main.stats.level = (int)load(3);
		main.stats.HP = (int)load(4);
	}
	
	public void saveGame()
	{
		StreamWriter writer = new StreamWriter("save.txt");
		
		writer.WriteLine(main.transform.position.x);
		writer.WriteLine(main.transform.position.z);
		writer.WriteLine(main.stats.level);
		writer.WriteLine(main.stats.HP);
		
		writer.Close();
	}
	
	float load(int lineNr)
	{
        FileInfo SourceFile = new FileInfo ("save.txt");
        StreamReader reader = SourceFile.OpenText();
		
		string data = "";
		
		for(int i = 0; i < lineNr; i++)
		{
        	data = reader.ReadLine();
		}
		
		reader.Close();
		
		return float.Parse(data);
	}
}
