using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	MyGravitation 	gravitation;
	
	public float 	elastizitaetsKoeffizient;
	
	// Use this for initialization
	void Start ()
	{
		gravitation = transform.GetComponent<MyGravitation>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown("space"))
			gravitation.v = 5;
		if(transform.position.y <= 0.5f)
		{
			gravitation.v *= -elastizitaetsKoeffizient;
			transform.position = new Vector3(0,0.5f,0);
		}
	}
}
