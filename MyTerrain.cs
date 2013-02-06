using UnityEngine;
using System.Collections;

public class MyTerrain : MonoBehaviour {
	
	const int 			WorldSize = 20;
	GameObject 			world;
	GameObject[,] 		terrain = new GameObject[WorldSize,WorldSize];
	public GameObject 	tile_prefab;
	
	void Start ()
	{
	}
	
	public void buidTerrain()
	{
		world = new GameObject();
		world.gameObject.name = "world";
		
		for(int i = 0; i < WorldSize; i++)
		{
			for(int j = 0; j < WorldSize; j++)
			{
				terrain[j,i] = Instantiate(tile_prefab, new Vector3((j - WorldSize / 2) * 3,0,(i - WorldSize / 2) * 3), Quaternion.identity) as GameObject;
				terrain[j,i].transform.parent = world.transform;
			}
		}
	}
	
}
