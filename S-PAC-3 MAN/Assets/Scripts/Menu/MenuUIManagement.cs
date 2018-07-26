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

	/* FPS 
	private CalculateFPS scriptFPS;
	private Text FPSText;
	*/

	/* Objectos de las distintas fases del menú principal */
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private GameObject pacman;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private Toggle checkMusic;
	[SerializeField] private Toggle checkSound;
	[SerializeField] private Toggle checkFPS;
	private bool musicBool;
	private bool soundBool;
	private bool FPSBool;
	[SerializeField] private GameObject creditsDisplay;
	[SerializeField] private GameObject instructionsDisplay1;
	[SerializeField] private GameObject instructionsDisplay2;
	[SerializeField] private GameObject instructionsDisplay3;

	void Start(){
		senseMS = 1;
		senseSS = 1;
		startPositionSS = speedSelector.transform.localPosition.y;
		startPositionMS = modeSelector.transform.localPosition.x;

		selectionMS = PlayerPrefs.GetInt("modeSelected", 1);
		selectionSS = PlayerPrefs.GetInt("speedSelected", 1);

		mainMenu.SetActive(true);
		optionsMenu.SetActive(false);
		creditsDisplay.SetActive(false);
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(false);


		if(PlayerPrefs.GetInt("musicOn", 1) == 1){
			checkMusic.isOn = true;
			musicBool = true;
			PlayerPrefs.SetInt("musicOn", 1);
		}else{
			checkMusic.isOn = false;
			musicBool = false;
		}

		if(PlayerPrefs.GetInt("soundOn", 1) == 1){
			checkSound.isOn = true;
			soundBool = true;
			PlayerPrefs.SetInt("soundOn", 1);
		}else{
			checkSound.isOn = false;
			soundBool = false;
		}

		if(PlayerPrefs.GetInt("FPSOn", 0) == 1){
			FPSCounter.SetActive(true);
			checkFPS.isOn = true;
			FPSBool = true;
			PlayerPrefs.SetInt("FPSOn", 0);
		}else{
			FPSCounter.SetActive(false);
			checkFPS.isOn = false;
			FPSBool = false;
		}
	}
	
	void Update () {
		MoveSelectors();
	}

	/* Mover las flechas de selección */
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

	/* Click de ready! */
	public void StartPlay(){
		PlayerPrefs.Save();
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
			PlayerPrefs.SetInt("speedSelected", selectionSS);
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
			PlayerPrefs.SetInt("speedSelected", selectionSS);
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
			PlayerPrefs.SetInt("speedSelected", selectionSS);
		}
	}

	/* Click de botones de selección de modo */
	public void MSNormal(){
		if(selectionMS != 1){
			modeSelector.transform.localPosition += Vector3.up * 143.9f;

			selectionMS = 1;
			PlayerPrefs.SetInt("modeSelected", selectionMS);
		}
	}
	public void MSNotEnoughOxygen(){
		if(selectionMS != 2){
			modeSelector.transform.localPosition += Vector3.up * -143.9f;

			selectionMS = 2;
			PlayerPrefs.SetInt("modeSelected", selectionMS);
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

		PlayerPrefs.Save();
	}

	public void CheckMusic(){
		int boolInt;
		if(musicBool){
			boolInt = 0;
			musicBool = false;
		}else{
			boolInt = 1;
			musicBool = true;
		}
		PlayerPrefs.SetInt("musicOn", boolInt);
	}

	public void CheckSound(){
		int boolInt;
		if(soundBool){
			boolInt = 0;
			soundBool = false;
		}else{
			boolInt = 1;
			soundBool = true;
		}
		PlayerPrefs.SetInt("soundOn", boolInt);
	}
	public void CheckFPS(){
		int boolInt;
		if(FPSBool){
			boolInt = 0;
			FPSBool = false;
		}else{
			boolInt = 1;
			FPSBool = true;
		}
		PlayerPrefs.SetInt("FPSOn", boolInt);
		
		FPSCounter.SetActive(FPSBool);
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

	public void InstructionsBt(){
		optionsMenu.SetActive(false);
		instructionsDisplay1.SetActive(true);
	}
	public void ExitInstructions1Bt(){
		instructionsDisplay1.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ExitInstructions2Bt(){
		instructionsDisplay2.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ExitInstructions3Bt(){
		instructionsDisplay3.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ContinueInstruction1Bt(){
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(true);
	}
	public void ContinueInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(true);
	}
	public void BackInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay1.SetActive(true);
	}
	public void BackInstruction3Bt(){
		instructionsDisplay3.SetActive(false);
		instructionsDisplay2.SetActive(true);
	}

	/* Click de GooglePlay */

	/* Click de records */
}
