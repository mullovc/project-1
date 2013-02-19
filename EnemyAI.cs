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
	
	
	
	// Use this for initialization
	void Start ()
	{
		main = Camera.main.GetComponent<Main>();
		stats = transform.GetComponent<Stats>();
		stats.statsOwner = this.transform;
		characterStats = main.character.GetComponent<Stats>();
	}
	
	void OnGUI()
	{
		//string HP = stats.HP.ToString();
		enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(enemyScreenPos.x - 10, Screen.height - enemyScreenPos.y - Screen.height * 0.15f,50,20),stats.HP.ToString());
	}
	
	void chaseCharacter()
	{
		Vector3 characterPos = main.character.transform.position; 
		
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
		
		if(!main.gameIsPaused && !(x < 1 && x > -1 && z < 1 && z > -1))
		{
			transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, z * Time.fixedDeltaTime),Space.World);
			animation.Play("run");
		}
		
		alpha = Mathf.Atan((characterPos.x - this.transform.position.x) / (characterPos.z - this.transform.position.z)) * (180 / Mathf.PI);
		if(z < 0)
			alpha += 180;
        transform.eulerAngles = new Vector3(0, alpha, 0);
	}
	
	void attack()
	{
		if(timeScinceLastAttack >= stats.attackRate && !main.gameIsPaused && (x < 1 && x > -1 && z < 1 && z > -1))
		{
			animation.Play("attack");
			characterStats.HP--;
			timeScinceLastAttack = 0;
		}
	}
	
	void die()
	{
		animation.Play("die");
		main.spawner[spawnedBySpawnerNr].spawnedNPCs--;
		Destroy(this.gameObject);
	}
	
	void getDamage()
	{
		animation.Play("gethit");
		stats.HP--;
	}
	
	void OnMouseDown()
	{
		getDamage();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!main.gameIsPaused)
			timeScinceLastAttack += Time.deltaTime;
		
		if(stats.HP <= 0)
		{
			die ();
		}
		
		chaseCharacter();
		attack();
	}
}