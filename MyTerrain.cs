using UnityEngine;
using System.Collections;

public class MyTerrain : MonoBehaviour {								//in dieser Klasse wird das Terrain gebaut, welches momentan 20*20 Kacheln, also Tiles sind
	
	const int 			WorldSize = 20;									//die Weltgroesse (die Welt ist immer quadratisch)
	GameObject 			world;											//dient als Parent fuer alle Tiles
	Transform[,] 		terrain = new Transform[WorldSize,WorldSize];	//der zweidimensionale-Array, welcher die Referenzen auf alle Tiles hat
	public Transform 	tile_prefab;									//das Prefab der Tiles. Aus ihnen besteht das Terrain
	
	void Start ()
	{
	}
	
	public void buidTerrain()
	{
		world = new GameObject();
		for(int i = 0; i < WorldSize; i++)
		{
			for(int j = 0; j < WorldSize; j++)
			{
				terrain[j,i] = Instantiate(tile_prefab, new Vector3((j - WorldSize / 2) * 3,0,(i - WorldSize / 2) * 3), Quaternion.identity) as Transform;	//die Kacheln werden im Abstand von 3 gesetzt. Es wird WorldSize/2 subtrahiert, damit die Kacheln um den Punkt(0|0) gebaut werden
				terrain[j,i].parent = world.transform;
			}
		}
	}
	
}
