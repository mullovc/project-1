using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Main		main;
	Stats			stats;
	
	public bool 	alternativeControl = false;
	Vector2 		initialMousePos;
	Vector2 		characterPos;
	
	void Start ()
	{
		stats = transform.GetComponent<Stats>();
		characterPos = Camera.main.WorldToScreenPoint(transform.position);
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 5, Screen.height * 0.75f / 2, 50, 20),stats.HP.ToString());
	}
	
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && alternativeControl)
		{
			initialMousePos = Input.mousePosition;
		}
		
		if(Input.GetMouseButton(0) && !main.gameIsPaused)
		{
			float x = 0, y = 0, alpha = 0;
			Vector2 mousePos = Input.mousePosition;
			
			if(alternativeControl)
				characterPos = initialMousePos;
			
			x = (mousePos.x - characterPos.x) * 0.05f;
			y = (mousePos.y - characterPos.y) * 0.05f;
			if(x > 10)
				x = 10;
			if(y > 10)
				y= 10;
			if(x < -10)
				x = -10;
			if(y < -10)
				y= -10;
			if(!alternativeControl || initialMousePos.x < Screen.width / 2)
				Camera.main.transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, y * Time.fixedDeltaTime),Space.World);
			
			alpha = Mathf.Atan((mousePos.x - characterPos.x) / (mousePos.y - characterPos.y)) * (180 / Mathf.PI);
			if(y < 0)
				alpha += 180;
        	transform.eulerAngles = new Vector3(0, alpha, 0);
		}
		if(stats.HP <= 0)
		{
			main.gameOver = true;
		}
			
	}
}
