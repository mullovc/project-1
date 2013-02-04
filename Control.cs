using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public GUISkin	buttonSkin;
	public bool 	alternativeControl = false;
	public Camera	mapCamera_prefab;
	Camera			mapCamera;
	
	bool 			menuIsOpen = false;
	bool 			gameIsPaused;
	Vector2 		initialMousePos;
	int				showMenuRegister = 0;
	
	void Start ()
	{
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;
		if(!menuIsOpen)
		{
			if(GUI.Button(new Rect(10,10,60,50),"Avatar"))
			{
				menuIsOpen = true;
				gameIsPaused = true;
			}
		}
		if(menuIsOpen)
		{
			GUI.Window(0,new Rect(20,20,Screen.width - 40, Screen.height - 40),menuWindow,"Menu");
		}
		
	}
	
	void menuWindow(int windowIndex)
	{
		
		GUI.BeginGroup(new Rect(20,20,Screen.width - 40,Screen.height - 40));
		{
			if(GUI.Button(new Rect(0,0,70,20),"Settings"))
			{
				showMenuRegister = 1;
			}
			if(GUI.Button(new Rect(70,0,70,20),"Map"))
			{
				showMenuRegister = 2;
			}
			if(GUI.Button(new Rect(140,0,70,20),"Stats"))
			{
				showMenuRegister = 3;
			}
			if(GUI.Button(new Rect(210,0,70,20),"Close"))
			{
				showMenuRegister = 0;
				menuIsOpen = false;
				gameIsPaused = false;
			}
			if(GUI.Button(new Rect(280,0,70,20),"Quit"))
			{
				Application.LoadLevel(0);
			}
			if(mapCamera != null && showMenuRegister != 2)
			{
				Destroy(mapCamera.gameObject);
				if(showMenuRegister != 0)
					gameIsPaused = true;
			}
			if(showMenuRegister == 1)
			{
				alternativeControl = GUI.Toggle(new Rect(0,30,150,20),alternativeControl,"Left screenhalf control");
			}
			if(showMenuRegister == 2)
			{
				if(mapCamera == null)
					mapCamera = Instantiate(mapCamera_prefab,mapCamera_prefab.transform.position,Quaternion.AngleAxis(90,Vector3.right)) as Camera;
				mapCamera.pixelRect = new Rect(40,40,Screen.width - 80,Screen.height - 100);
				gameIsPaused = false;
			}
		}
		GUI.EndGroup();
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && alternativeControl)
		{
			initialMousePos = Input.mousePosition;
		}
		
		if(Input.GetMouseButton(0) && !gameIsPaused)
		{
			Vector2 characterPos = Camera.main.WorldToScreenPoint(this.transform.position);
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
	}
}
