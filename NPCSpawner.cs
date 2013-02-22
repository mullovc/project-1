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
	public int			minSpawnRadius;
	public int			maxSpawnRadius;
	public float		activityRadius;
	public bool			spawnerIsActive;
	public int			NPCLevel;
	public float		enemyAttackRate;
	public float		enemyAttackRange;
	
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
				float alpha = rand.Next(180) - 90;
				float a = rand.Next(minSpawnRadius,maxSpawnRadius);
				float spawnPosX = Mathf.Cos(alpha * (Mathf.PI / 180.0f)) * a;
				float spawnPosZ =  Mathf.Sin(alpha * (Mathf.PI / 180.0f)) * a;
				if(rand.Next(2) == 1)
					spawnPosX *= -1;
				
				enemy[spawnedNPCs] = Instantiate(enemyModel,new Vector3(		// -->
				main.character.transform.position.x + spawnPosX,1,				// -->
				main.character.transform.position.z + spawnPosZ),				// -->
				Quaternion.identity) as GameObject;
				
				EnemyAI ai = enemy[spawnedNPCs].GetComponent<EnemyAI>();
				ai.spawnedBySpawnerNr = spawnerID;
				Stats enemyStats = enemy[spawnedNPCs].GetComponent<Stats>();
				enemyStats.level = NPCLevel;
				enemyStats.attackRate = enemyAttackRate;
				enemyStats.attackRange = enemyAttackRange;
				
				spawnedNPCs++;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Mathf.Sqrt((main.character.transform.position.x - transform.position.x)
		* (main.character.transform.position.x - transform.position.x)
		+ (main.character.transform.position.z - transform.position.z)
		* (main.character.transform.position.z - transform.position.z)) < activityRadius)
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
			if(spawnedNPCs < spawnLimit && hostileSpawner)
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
		if(transform.FindChild("spawnArea").transform.localScale != new Vector3(activityRadius * 2,0.1f,activityRadius * 2))
			transform.FindChild("spawnArea").transform.localScale = new Vector3(activityRadius * 2,0.1f,activityRadius * 2);
	}
}