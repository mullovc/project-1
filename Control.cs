using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Main		main;
	
	public bool 	alternativeControl = false;
	Vector2 		referencePos;
	//float			pressLength;
	Vector2 		mousePos;
	
	
	void moveCharacter(Vector2 referencePoint,Vector2 walkToPoint)
	{
		if(!main.gameIsPaused)
		{
			float x = (walkToPoint.x - referencePos.x) * 0.1f;
			float y = (walkToPoint.y - referencePos.y) * 0.1f;
			float alpha = 0;
			if(x > 10)
				x = 10;
			if(y > 10)
				y= 10;
			if(x < -10)
				x = -10;
			if(y < -10)
				y= -10;
			
			Camera.main.transform.Translate(new Vector3(x * Time.fixedDeltaTime, 0, y * Time.fixedDeltaTime),Space.World);
			
			if(referencePos != walkToPoint)
				alpha = Mathf.Atan((walkToPoint.x - referencePos.x) / (walkToPoint.y - referencePos.y)) * (180 / Mathf.PI);
			
			if(y < 0)
				alpha += 180;
	        transform.eulerAngles = new Vector3(0, alpha, 0);
		}
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && alternativeControl)
		{
			referencePos = Input.mousePosition;
		}
		/*
		if(Input.GetMouseButtonDown(0))
		{
			pressLength = 0;
		}
		*/
		if(Input.GetMouseButton(0))
		{
			mousePos = Input.mousePosition;
			
			if(!alternativeControl)
				referencePos = Camera.main.WorldToScreenPoint(transform.position);
			
			moveCharacter(referencePos,mousePos);
			
			//pressLength += Time.deltaTime;
		}
		/*
		else if(pressLength < 0.3f && pressLength > 0)
		{
			if(Input.GetMouseButtonUp(0))
			{
				mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
				mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).z + 5;
			}
			moveCharacter(referencePos, Camera.main.WorldToScreenPoint(mousePos));
		}
		*/
	}
}
