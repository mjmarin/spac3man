using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateFPS : MonoBehaviour {

	/* Cálculo */
	[SerializeField] private float updateRate;
	private int frameCount;
 	private float deltatime;
 	private float fps;

	 /* Display */
	[SerializeField] private GameObject FPSCounter;
	private Text FPSText;
 	
	private void Awake(){
		FPSText = FPSCounter.GetComponent<Text>();
	}
	void Start () {
		frameCount = 0;
		deltatime = 0.0f;
		fps = 0.0f;
	}

	void Update () {
		Calculate();
		Display();
	}

	private void Calculate(){
		frameCount++;
     	deltatime += Time.deltaTime;
     	if (deltatime > 1.0/updateRate){		/* updateRate no es preciso, es más o menos el número de actualizaciones por segundo */
        	fps = frameCount / deltatime;
        	frameCount = 0;
        	deltatime = 0;
     	}
	}

	private void Display(){
		FPSText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
	}
}
