using UnityEngine;
using System.Collections;

public class MyGravitation : MonoBehaviour {
	
	public float v;
	public float t;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	float getTravelDistance()
	{
		float s = 0;
		v += Time.deltaTime * (-9.81f);
		s = v * t - v * (t - Time.deltaTime);
		return s;
	}
	
	// Update is called once per frame
	void Update ()
	{
		t += Time.deltaTime;
		transform.Translate(0,getTravelDistance(),0);
	}
}
