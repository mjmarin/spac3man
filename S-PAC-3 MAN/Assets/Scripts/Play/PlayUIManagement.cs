using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayUIManagement : MonoBehaviour {

	/*--------------------- Objetos de la escena ------------------------*/
	[SerializeField] private GameObject gameOverText;
	[SerializeField] private GameObject pauseText;
	[SerializeField] private GameObject pauseBt;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private Text counterText;
	[SerializeField] private Text timerText;
	[SerializeField] private GameObject score;

	/*---------------------- Variables menú pausa ------------------------*/
	[SerializeField] private Sprite playSprite;
	[SerializeField] private Sprite pauseSprite;
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Button soundBt;
	[SerializeField] private Sprite musicOnSprite;
	[SerializeField] private Sprite musicOffSprite;
	[SerializeField] private Button musicBt;

	/*---------------------- Variables final de partida ------------------------*/
	[SerializeField] private Text scoreMoneyText;
	[SerializeField] private Text scoreTimerText;
	[SerializeField] private Text scoreValueText;
	[SerializeField] private Text addTimeText;
	[SerializeField] private GameObject newRecordText;
	[SerializeField] private GameObject questRewardText;

	/*-------------------- Scripts con los que se comunica ----------------------*/
	private PlayerController playerScript;
	private Timer timerScript;
	
	/*-------------------- Variables para temporizador ----------------------------*/
	private int timerMinutes;
	private int timerSeconds;

	private int NEOtimerMinutes;
	private int NEOtimerSeconds;

	/*------------- Variables de efecto gráfico en incremento de tiempo en NEO -------------*/
	private float addTimeAlpha;
	[SerializeField] private float alphaProgression;

	/* Variable que señala la pantalla actual */
	private int currentWindow = 0;


	/* Inicialización de referencias */
	private void Awake(){
		playerScript = player.GetComponent<PlayerController>();
		timerScript = Camera.main.GetComponent<Timer>();
	}

	/* Inicialización de variables */
	private void Start () {
		currentWindow = 0;

		if(DataManager.GetSelectionMS() == 1){
			addTimeText.gameObject.SetActive(false);
		}else{
			addTimeText.gameObject.SetActive(true);
			addTimeText.enabled = false;
		}
		gameOverText.SetActive(false);
		pauseText.SetActive(false);
		pauseMenu.SetActive(false);
		score.SetActive(false);
		newRecordText.SetActive(false);
		questRewardText.SetActive(false);

		if(DataManager.GetFPSOn()){
			FPSCounter.SetActive(true);
		}else{
			FPSCounter.SetActive(false);
		}

		if(DataManager.GetMusicOn()){
			musicBt.GetComponent<Image>().sprite = musicOnSprite;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOffSprite;
		}
		
		if(DataManager.GetSoundOn()){
			soundBt.GetComponent<Image>().sprite = soundOnSprite;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOffSprite;
		}
	}
	
	/* 
	Listener de botón back nativo de Android.
	Actualización del temporizador gráfico de partida. 
	Actualización de marcador de monedas.
	Control de pantalla de fin de partida.
	Control del texto de adición de tiempo para modo NEO
	*/
	private void Update(){

		BackListener();

		UpdateTimer();

		counterText.text = playerScript.GetPickUps().ToString("F0");
		
		if(playerScript.GetDeath()){		
			SetDeathMenu();
		}

		if(addTimeText.isActiveAndEnabled){
			addTimeText.color = addTimeText.color = new Vector4(1.0f, 0.0f, 0.0f, addTimeAlpha);
			if(addTimeAlpha > 0){
				addTimeAlpha -= alphaProgression * Time.deltaTime;
			}
		}
	}

	/* Función que controla el uso del botón back nativo de Android */
    private void BackListener(){
        if(Input.GetKeyDown(KeyCode.Escape)){
			switch(currentWindow){
				case 0:
					PauseBt();
				break;
				case 1:
					PauseBt();
				break;
				case 2:
					MenuBt();
				break;
			}
		}
	}

	/* Función que actualiza el temporizador gráfico */
    private void UpdateTimer(){
		if(playerScript.GetDeath() == false){
			timerSeconds = Mathf.FloorToInt(timerScript.GetTime());
			timerMinutes = timerSeconds / 60;

			if(DataManager.GetSelectionMS() == 2){
				NEOtimerSeconds = (int)Mathf.Floor(timerScript.GetNeoTime());
				NEOtimerMinutes = NEOtimerSeconds / 60;

				timerText.text = string.Format("{0:D2}:{1:D2}", NEOtimerMinutes, NEOtimerSeconds % 60);
			}else{
				timerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
			}
		}
	}

	/* Función que inicia la pantalla de fin de partida */
	private void SetDeathMenu(){
		currentWindow = 2;
		pauseBt.SetActive(false);
		gameOverText.SetActive(true);
		pauseMenu.SetActive(true);
		timerText.gameObject.SetActive(false);
		counterText.transform.parent.gameObject.SetActive(false);
		SetScore();
		score.SetActive(true);	
	}

	/* Modificador de texto de puntuación */
	private void SetScore(){
		scoreMoneyText.text = counterText.text;
		scoreTimerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
		scoreValueText.text = Mathf.FloorToInt(playerScript.GetCurrentScore()).ToString();
	}

	/* Función interfaz que activa el aviso de nuevo record */
	public void UpNewRecordText(){
		newRecordText.SetActive(true);
	}

	/* Función interfaz que activa el aviso por recompensa de misones completadas */
	public void UpQuestRewardText(float reward){
		questRewardText.GetComponent<Text>().text = "Quest reward:" + reward.ToString("F0");
		questRewardText.SetActive(true);
	}

	/* Función interfaz */
	public void EnlargeNeoTimeText(float seconds){ /* PARA CLASE TIMER */
		addTimeText.enabled = true;
		addTimeText.text = "+ " + Mathf.Round(seconds).ToString("") + "s";
		addTimeAlpha = 1.0f;
	}

	/*----------------- Funciones ligadas al evento OnClick() de los botones ------------------*/

	/* Función de apertura y cierre de la pantalla de pausa */
	public void PauseBt(){
		if(timerScript.GetPaused()){
			currentWindow = 1;
			pauseText.SetActive(true);
			pauseMenu.SetActive(true);
			pauseBt.GetComponent<Image>().sprite = playSprite;
		}else{
			currentWindow = 0;
			pauseText.SetActive(false);
			pauseMenu.SetActive(false);
			pauseBt.GetComponent<Image>().sprite = pauseSprite;
		}
	}

	/* Función de reinicio de partida */
	public void ReloadBt(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/* Función de apertura de la pantalla de menú principal */
	public void MenuBt(){
		if(player.GetComponent<PlayerController>().GetDeath() == false)
			timerScript.ChangeState();
		SceneManager.LoadScene(0);
	}

	/* Función de cierre de la aplicación */
	public void QuitBt(){
		Application.Quit();
	}

	/* Función de activación/desactivación de efectos de sonido en la aplicación */
	public void SoundBt(){
		if(DataManager.GetSoundOn()){
			soundBt.GetComponent<Image>().sprite = soundOffSprite;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOnSprite;
		}
	}

	/* Función de activación/desactivación de audios de música en la aplicación */
	public void MusicBt(){
		if(DataManager.GetMusicOn()){
			musicBt.GetComponent<Image>().sprite = musicOffSprite;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOnSprite;
		}
	}
}
