using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Main			main;
	
	public Stats 		stats;
	Stats				characterStats;
	
	float 				x;
	float 				z;
	float 				alpha;
	public float		timeScinceLastAttack;
	Vector2				enemyScreenPos;
	public int			spawnedBySpawnerNr;
	bool				spottedPlayer;
	bool				dead;
	
	public Texture2D	healthBar;
	
	
	// Use this for initialization
	void Start ()
	{
		main = Camera.main.GetComponent<Main>();
		stats = transform.GetComponent<Stats>();
		stats.statsOwner = transform;
		stats.updateStats(true);
		characterStats = main.stats;
	}
	
	void OnGUI()
	{
		if(!main.gameIsPaused && !dead)
		{
			GUI.skin.box.normal.background = healthBar;
			enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);
			GUI.Box(new Rect(enemyScreenPos.x - stats.HP * 5 / 2,Screen.height - enemyScreenPos.y - Screen.height * 0.15f,stats.HP * 5,10),"");
		}
	}
	
	float getDistance(Transform object1,Transform object2)
	{
		return Mathf.Sqrt((object2.position.x - object1.position.x)
		* (object2.position.x - object1.position.x)
		+ (object2.position.z - object1.position.z)
		* (object2.position.z - object1.position.z));
	}
	
	void walkTo(Transform objectToChase)
	{
		Vector3 characterPos = objectToChase.position; 
		
		x = (characterPos.x - this.transform.position.x) * 0.8f;
		z = (characterPos.z - this.transform.position.z) * 0.8f;
		if(x > 10)
			x = 10;
		if(z > 10)
			z = 10;
		if(x < -10)
			x = -10;
		if(z < -10)
			z = -10;
		
		if(!main.gameIsPaused && getDistance(objectToChase,transform) > stats.attackRange)
		{
			transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, z * Time.fixedDeltaTime),Space.World);
			animation.Play("run");
		}
		
		alpha = Mathf.Atan((characterPos.x - transform.position.x) / (characterPos.z - transform.position.z)) * (180 / Mathf.PI);
		if(z < 0)
			alpha += 180;
        transform.eulerAngles = new Vector3(0, alpha, 0);
	}
	
	void attack()
	{
		if(timeScinceLastAttack >= stats.attackRate && getDistance(main.character.transform,transform) <= stats.attackRange)
		{
			animation.Play("attack");
			if(!main.godMode)
				characterStats.HP -= stats.level;
			timeScinceLastAttack = 0;
		}
	}
	
	bool spot(Transform objectToBeSpotted)
	{
		float dx = objectToBeSpotted.transform.position.x - transform.position.x;
		float dz = objectToBeSpotted.transform.position.z - transform.position.z;
		
		float alpha = Mathf.Atan(dx / dz) / (Mathf.PI / 180.0f);
		
		if(dz < 0)
			alpha += 180;
		if(dz > 0 && dx < 0)
			alpha += 360;
		
		if(alpha < (stats.spotAngle / 2 + transform.rotation.eulerAngles.y)
		&& alpha > (-stats.spotAngle / 2 + transform.rotation.eulerAngles.y)
		&& Mathf.Sqrt(dx * dx + dz * dz) <= stats.spotDistance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	void die()
	{
		if(dead && !animation.IsPlaying("die"))
		{
			main.stats.exp += stats.expGain;
			main.spawner[spawnedBySpawnerNr].spawnedNPCs--;
			Destroy(this.gameObject);
		}
		dead = true;
		animation.Play("die");
	}
	
	void getDamage()
	{
		animation.PlayQueued("gethit");
		stats.HP -= characterStats.level;
		spottedPlayer = true;
	}
	
	void OnMouseDown()
	{
		getDamage();
	}
	
	void Update ()
	{
		if(!dead)
		{
			if(!main.gameIsPaused)
				timeScinceLastAttack += Time.deltaTime;
			
			
			if(!spottedPlayer)
				spottedPlayer = spot(main.character.transform);
			
			if(spottedPlayer)
			{
				walkTo(main.character.transform);
				attack();
			}
		}
		
		if(stats.HP <= 0 && stats.maxHP > 0)
		{
			die ();
		}
	}
}