using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateFPS : MonoBehaviour {

	[SerializeField] private float updateRate;
	private int frameCount = 0;
 	private float deltatime = 0.0f;
 	private float fps = 0.0f;
 	
	void Start () {
		frameCount = 0;
		deltatime = 0.0f;
		fps = 0.0f;
	}

	void Update () {
		frameCount++;
     	deltatime += Time.deltaTime;
     	if (deltatime > 1.0/updateRate){		/* updateRate no es preciso, es más o menos el número de actualizaciones por segundo */
        	fps = frameCount / deltatime;
        	frameCount = 0;
        	deltatime = 0;
     	}
	}

	public int GetFPS(){
		return Mathf.RoundToInt(fps);
	}
}
