using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public Transform 	statsOwner;
	
	public int 			HP;
	public float 		attackRate;
	public int			maxHP;
	public int			level;
	public float		attackRange;
	
	
	// Use this for initialization
	void Start ()
	{
		maxHP = level * 10;
		HP = maxHP;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
