using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour {
	
	public Main			main;
	
	public GameObject 	knight_prefab;
	public GameObject 	archer_prefab;
	public GameObject 	merchant_prefab;
	public GameObject	mage_prefab;
	
	
	GameObject			enemyModel;
	GameObject[]		enemy = new GameObject[100];
	
	System.Random 		rand = new System.Random();
	public bool 		hostileSpawner;
	public int			spawnedNPCs;
	public int 			spawnLimit;
	public int			spawnRate;
	public int			spawnerID;
	public float		activityArea;
	public bool			spawnerIsActive;
	
	public NPCType 		spawnType;
	
	
	// Use this for initialization
	void Start ()
	{
		main = Camera.main.GetComponent<Main>();
		main.spawner[spawnerID] = this;
		spawnedNPCs = 0;
		
		switch(spawnType)
		{
			case NPCType.Merchant:
				enemyModel = merchant_prefab;
				break;
			case NPCType.Knight:
				enemyModel = knight_prefab;
				break;
			case NPCType.Archer:
				enemyModel = archer_prefab;
				break;
			case NPCType.Mage:
				enemyModel = mage_prefab;
				break;
		}
	}
	
	public enum NPCType {
		Merchant,
		Archer,
		Knight,
		Mage
	}
	
	
	void spawnNPC()
	{
		for(int i = 0; i <= spawnLimit; i++)
		{
			if(enemy[i] == null)
			{
				float spawnPosX = Camera.main.transform.position.x + rand.Next(10) - 5;
				float spawnPosZ = Camera.main.transform.position.z + rand.Next(10);
				enemy[spawnedNPCs] = Instantiate(enemyModel,new Vector3(spawnPosX,1,spawnPosZ),Quaternion.identity) as GameObject;
				
				enemy[spawnedNPCs].GetComponent<EnemyAI>().spawnedBySpawnerNr = spawnerID;
				
				
				spawnedNPCs++;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if((main.character.transform.position.x - transform.position.x) < activityArea 
		&& (main.character.transform.position.x - transform.position.x) > -activityArea
		&& (main.character.transform.position.z - transform.position.z) < activityArea 
		&& (main.character.transform.position.z - transform.position.z) > -activityArea)
			spawnerIsActive = true;
		else
		{
			spawnerIsActive = false;
			
			for(int i = spawnedNPCs; i > 0; i--)
			{
				Destroy(enemy[i - 1].gameObject);
				spawnedNPCs--;
			}
			
		}
		
		if(spawnerIsActive)
		{
			if(spawnedNPCs < spawnLimit)
			{
				int spawnChance = rand.Next(spawnRate);
				
				if(spawnChance == 0)
					spawnNPC();
			}
			
			if(spawnedNPCs > spawnLimit)
			{
				for(int i = spawnedNPCs; i > spawnLimit; i--)
				{
					Destroy(enemy[i - 1].gameObject);
					spawnedNPCs--;
				}
			}
		}
	}
}