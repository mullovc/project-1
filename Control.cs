using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Main		main;
	Stats			stats;
	
	public bool 	alternativeControl = false;
	Vector2 		referencePos;
	
	float		 	x;
	float			y;
	float			alpha;
	
	void Start ()
	{
		stats = transform.GetComponent<Stats>();
	}
	
	void OnGUI()
	{
		if(!main.gameIsPaused)
		{
			float w = Screen.width;
			float h = Screen.height;
			GUI.Label(new Rect(w / 2 - 5, h * 0.75f / 2, w * 0.6f, h * 0.3f),stats.HP.ToString());
		}
	}
	
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && alternativeControl)
		{
			referencePos = Input.mousePosition;
		}
		
		if(Input.GetMouseButton(0) && !main.gameIsPaused)
		{
			Vector2 mousePos = Input.mousePosition;
			
			if(!alternativeControl)
				referencePos = Camera.main.WorldToScreenPoint(transform.position);
			
			x = (mousePos.x - referencePos.x) * 0.05f;
			y = (mousePos.y - referencePos.y) * 0.05f;
			if(x > 10)
				x = 10;
			if(y > 10)
				y= 10;
			if(x < -10)
				x = -10;
			if(y < -10)
				y= -10;
			if(!alternativeControl || referencePos.x < Screen.width / 2)
				Camera.main.transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, y * Time.fixedDeltaTime),Space.World);
			
			if(referencePos != mousePos)
				alpha = Mathf.Atan((mousePos.x - referencePos.x) / (mousePos.y - referencePos.y)) * (180 / Mathf.PI);
			
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
