using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManagement : MonoBehaviour {

	/* Variables movimiento flechas */
	[SerializeField] private GameObject speedSelector;
	[SerializeField] private GameObject modeSelector;
	[SerializeField] private float range;
	[SerializeField] private float speed;
	private float startPositionSS;
	private float startPositionMS;
	private int senseSS;
	private int senseMS;
	private int selectionSS;
	private int selectionMS;

	/* Configuración del juego */
	private Configuration scriptConfiguration;

	/* FPS */
	private CalculateFPS scriptFPS;
	private Text FPSText;

	/* Objectos de las distintas fases del menú principal */
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private GameObject pacman;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject creditsDisplay;

	void Start(){
		senseMS = 1;
		senseSS = 1;
		startPositionSS = speedSelector.transform.localPosition.y;
		startPositionMS = modeSelector.transform.localPosition.x;

		selectionMS = 1;
		selectionSS = 1;

		scriptConfiguration = Camera.main.GetComponent<Configuration>();

		optionsMenu.SetActive(false);
		creditsDisplay.SetActive(false);
		FPSCounter.SetActive(false);

		FPSText = FPSCounter.GetComponent<Text>();
		scriptFPS = GetComponent<CalculateFPS>();
	}
	
	void Update () {
		MoveSelectors();
		DisplayFPS();
	}

	private void MoveSelectors(){
		senseSS = Sense(speedSelector.transform.localPosition.y, startPositionSS, senseSS);
		speedSelector.transform.localPosition += Vector3.up * speed * senseSS;

		senseMS = Sense(modeSelector.transform.localPosition.x, startPositionMS, senseMS);
		modeSelector.transform.localPosition += Vector3.right * speed * senseMS;
	}

	private int Sense(float localPosition, float startPosition, int sense){
		if((localPosition > startPosition + range && sense == 1) || (localPosition < startPosition - range && sense == -1)){
			return -sense;
		}else{
			return sense;
		}
	}

	/* Display fps */
	public void SetFPS(bool boolean){
		FPSCounter.SetActive(boolean);
	}

	private void DisplayFPS(){
		FPSText.text = "FPS: " + scriptFPS.GetFPS().ToString();
	}

	/* Click de ready! */
	public void StartPlay(){
		SceneManager.LoadScene(1);
	}

	/* Click de botones de selección de velocidad */
	public void SSNormal(){
		if(selectionSS != 1){
			if(selectionSS == 2){
				speedSelector.transform.localPosition += Vector3.right * -242;		//242 es la distancia entre los centros de los botones
			}else{
				speedSelector.transform.localPosition += Vector3.right * -484;
			}

			selectionSS = 1;
			scriptConfiguration.SetSpeedSelected(selectionSS);
		}
	}
	public void SSSpeedRacer(){
		if(selectionSS != 2){
			if(selectionSS == 3){
				speedSelector.transform.localPosition += Vector3.right * -242;
			}else{
				speedSelector.transform.localPosition += Vector3.right * 242;
			}

			selectionSS = 2;
			scriptConfiguration.SetSpeedSelected(selectionSS);
		}
	}
	public void SSTryhard(){
		if(selectionSS != 3){
			if(selectionSS == 1){
				speedSelector.transform.localPosition += Vector3.right * 484;
			}else{
				speedSelector.transform.localPosition += Vector3.right * 242;
			}

			selectionSS = 3;
			scriptConfiguration.SetSpeedSelected(selectionSS);
		}
	}

	/* Click de botones de selección de modo */
	public void MSNormal(){
		if(selectionMS != 1){
			modeSelector.transform.localPosition += Vector3.up * 143.9f;

			selectionMS = 1;
			scriptConfiguration.SetModeSelected(selectionMS);
		}
	}
	public void MSNotEnoughOxygen(){
		if(selectionMS != 2){
			modeSelector.transform.localPosition += Vector3.up * -143.9f;

			selectionMS = 2;
			scriptConfiguration.SetModeSelected(selectionMS);
		}
	}

	/* Click de ajustes */
	public void OptionsBt(){
		mainMenu.SetActive(false);
		pacman.SetActive(false);
		optionsMenu.SetActive(true);
	}

	public void ExitOptionsBt(){
		mainMenu.SetActive(true);
		pacman.SetActive(true);
		optionsMenu.SetActive(false);

		/* Guardar opciones para jugador */

	}

	public void CreditsBt(){
		optionsMenu.SetActive(false);
		creditsDisplay.SetActive(true);
	}

	public void BackCreditsBt(){
		creditsDisplay.SetActive(false);
		optionsMenu.SetActive(true);
	}

	/* Click de misiones */

	/* Click de instrucciones */

	/* Click de ajustes */

	/* Click de GooglePlay */

	/* Click de records */
}
