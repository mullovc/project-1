using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour {
	
	public Main			main;
	
	public GameObject 	enemyModel_prefab;
	GameObject[]		enemy = new GameObject[10];
	
	System.Random 		rand = new System.Random();
	bool 				hostile;
	int					spawnedEnemies;
	public int 			spawnLimit;
	public int			spawnRate;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	void spawnHostile()
	{
		if(spawnedEnemies < spawnLimit)
		{
			float spawnPosX = Camera.main.transform.position.x + rand.Next(10) - 5;
			float spawnPosZ = Camera.main.transform.position.z + rand.Next(10);
			enemy[spawnedEnemies] = Instantiate(enemyModel_prefab,new Vector3(spawnPosX,1,spawnPosZ),Quaternion.identity) as GameObject;
			
			spawnedEnemies++;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		int spawnChance = rand.Next(spawnRate);
		
		if(spawnChance == 0)
			spawnHostile();
	}
}
