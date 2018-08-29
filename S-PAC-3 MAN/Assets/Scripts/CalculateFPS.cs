using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateFPS : MonoBehaviour {

	/* Ratio de actualización del valor de frames por segundo */
	[SerializeField] private float updateRate;

	/* Contador de frames transcurridos en cada actualización */
	private int frameCount;

	/* Temporizador */
 	private float deltatime;

	/* Resultado del cálculo */
 	private float fps;

	/* Referencias a texto de display */
	[SerializeField] private GameObject FPSCounter;
	private Text FPSText;
 	
	/* Inicialización de referencias */
	private void Awake(){
		FPSText = FPSCounter.GetComponent<Text>();
	}

	/* Inicialización de variables */
	void Start () {
		frameCount = 0;
		deltatime = 0.0f;
		fps = 0.0f;
	}

	/* Cálculo y muestra de datos */
	void Update () {
		Calculate();
		Display();
	}

	/* Cálculo de frames por segundo */
	private void Calculate(){
		frameCount++;
     	deltatime += Time.deltaTime;
     	if (deltatime > 1.0/updateRate){		/* updateRate no es preciso, es más o menos el número de actualizaciones por segundo */
        	fps = frameCount / deltatime;
        	frameCount = 0;
        	deltatime = 0;
     	}
	}

	/* Muestra de frames por segundo */
	private void Display(){
		FPSText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
	}
}
