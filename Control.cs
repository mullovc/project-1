using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {					//von der Klasse hab ich schon erzählt. Sie ist zustaendig für die Steuerung des Characters. Ungeschickterweise habe ich aber auch die GUI fuer das Ingame-Menu hier reingepackt
	
	public GUISkin	buttonSkin;							//das Prefab für den Skin der Menu-Buttons
	public bool 	alternativeControl = false;			//es gibt zwei verschiedene Steuerungen. Bei true benutzt das Spiel die eine Steuerung, bei false die andere
	public Camera	mapCamera_prefab;					//das Prefab für die Kamera, mit welcher man die Map (über das Ingame-Menu) sieht
	Camera			mapCamera;							//die Instanz der Map-Kamera
	
	bool 			menuIsOpen = false;
	bool 			gameIsPaused;						//wenn true, dann ist das Laufen deaktiviert
	Vector2 		initialMousePos;					//wenn die alternative Steuerung an ist, ist das hier der Bezugspunkt, um die Laufgeschwindigkeit zu gerechnen
	int				showMenuRegister = 0;				//hier wird vermerkt, welches Register im Ingame-Menu offen ist. Bei 0 ist kein Register offen
	
	void Start ()
	{
	}
	
	void OnGUI()
	{
		GUI.skin = buttonSkin;							//der aktuelle Skin der Buttons wird auf buttonSkin gesetzt 
		if(!menuIsOpen)									//dieser Button soll nur angezeigt werden, wenn das Menu geschlossen ist
		{
			if(GUI.Button(new Rect(10,10,60,50),"Avatar"))			//dieser Button wird wärend des Spiels oben links angezeigt
			{
				menuIsOpen = true;
				gameIsPaused = true;
			}
		}
		if(menuIsOpen)
		{
			GUI.Window(0,new Rect(20,20,Screen.width - 40, Screen.height - 40),menuWindow,"Menu");		//das Menufenster wird geöffnet und mit "menuWindow(int)" gebaut. Das Fenster hat den Fensterindex 0
		}
		
	}
	
	void menuWindow(int windowIndex)
	{
		
		GUI.BeginGroup(new Rect(20,20,Screen.width - 40,Screen.height - 40));
		{
			if(GUI.Button(new Rect(0,0,70,20),"Settings"))			//ich weiss, folgendes haette ich auch alles mit einer Switch machen können, ich war aber zu faul
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
			if(mapCamera != null && showMenuRegister != 2)	//die Kamera wird zerstört, wenn noch eine da ist, showMenuRegister aber nicht mehr gleich 2 ist
			{
				Destroy(mapCamera.gameObject);
				if(showMenuRegister != 0)
					gameIsPaused = true;
			}
			if(showMenuRegister == 1)
			{
				alternativeControl = GUI.Toggle(new Rect(0,30,150,20),alternativeControl,"Left screenhalf control");		//hier wird die Option angezeigt, die Steuerung zu aendern
			}
			if(showMenuRegister == 2)
			{
				if(mapCamera == null)			//die Kamera soll nur instanziert werden, wenn noch keine da ist. Sonst wird jeden Frame eine neue Kamera erstellt und du kannst dir ja vorstellen, was dabei raus kommt
					mapCamera = Instantiate(mapCamera_prefab,mapCamera_prefab.transform.position,Quaternion.AngleAxis(90,Vector3.right)) as Camera;
				mapCamera.pixelRect = new Rect(40,40,Screen.width - 80,Screen.height - 100);		//die Kamera soll nur einen Teil des Bildschirms einnehmen
				gameIsPaused = false;
			}
		}
		GUI.EndGroup();
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && alternativeControl)		//falls die Steuerung 2 aktiv ist, wird hier der Bezugspunkt gesetzt, wenn man die Maus klickt. Wenn man dann vom Bezugspunkt aus die Maus z.B. nach Links bewegt, laeuft man nach links
		{
			initialMousePos = Input.mousePosition;
		}
		
		if(Input.GetMouseButton(0) && !gameIsPaused)
		{
			Vector2 characterPos = Camera.main.WorldToScreenPoint(this.transform.position);		//die Position des Characters auf dem Bildschirm (in Pixeln)
			float x = 0, y = 0, alpha = 0;
			Vector2 mousePos = Input.mousePosition;												//die Position der Maus auf dem Bildschirm (in Pixeln)
			
			if(alternativeControl)
				characterPos = initialMousePos;
			
			x = (mousePos.x - characterPos.x) * 0.05f;						//die Laufgeschwindligkeit. Multipliziert mit 0.05, damit es nicht zu schnell ist
			y = (mousePos.y - characterPos.y) * 0.05f;
			if(x > 10)														//je weiter die maus von characterPos entfernt ist, desto schnelle bewegt dieser sich. Deshalt wird die Geschwindigkeit auf 10 begrenzt
				x = 10;
			if(y > 10)
				y= 10;
			if(x < -10)
				x = -10;
			if(y < -10)
				y= -10;
			if(!alternativeControl || initialMousePos.x < Screen.width / 2)			//falls Steuerung 2 aktiv ist, darf InitialMousePos nur auf der linken Bildschirmhälfte sein
				Camera.main.transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, y * Time.fixedDeltaTime),Space.World);		//die Kamera wird auf der x/z-Achse der Welt bewegt, da die Achsen der Kamera verschoben sind
			
			alpha = Mathf.Atan((mousePos.x - characterPos.x) / (mousePos.y - characterPos.y)) * (180 / Mathf.PI);		//Mathf.Atan() = arctan(), Radiant. Um von radiant zu degree umzurechnen, multipliziere ich mit 180/PI
			if(y < 0)											//das, was ich in Kunst erklärt habe
				alpha += 180;
        	transform.eulerAngles = new Vector3(0, alpha, 0);	//rotation der y-Achse auf alpha setzen
			
		}
	}
}
