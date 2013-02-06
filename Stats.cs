using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public Transform 	statsOwner;
	
	public int 			HP;
	public float 		attackRate;
	
	
	// Use this for initialization
	void Start ()
	{
		HP = 10;
		attackRate = 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
