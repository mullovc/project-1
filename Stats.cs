using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public Transform 	statsOwner;
	
	public int 			HP;
	public float 		attackRate;
	public int			maxHP;
	public int			level;
	public float		attackRange;
	public float		spotAngle;
	public float		spotDistance;
	public int			exp;
	public int			expForNextLevel;
	
	public int			expGain;
	
	
	// Use this for initialization
	void Start ()
	{
	}
	
	public void updateStats(bool allStats)
	{
		maxHP = level * 10;
		expForNextLevel = level * 10;
		if(allStats)
		{
			HP = maxHP;
		}
		
		if(exp >= expForNextLevel)
			levelUp();
	}
	
	void levelUp()
	{
		exp = 0;
		level++;
		updateStats(true);
	}
	
	void Update ()
	{
		updateStats(false);
	}
}
