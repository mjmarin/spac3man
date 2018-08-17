using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayUIManagement : MonoBehaviour {
	/* Objetos de la escena */
	[SerializeField] private GameObject gameOverText;
	[SerializeField] private GameObject pauseText;
	[SerializeField] private GameObject pauseBt;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private Text counterText;
	[SerializeField] private Text timerText;
	[SerializeField] private GameObject score;
	/* Variables menú pausa */
	[SerializeField] private Sprite playSprite;
	[SerializeField] private Sprite pauseSprite;
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Button soundBt;
	[SerializeField] private Sprite musicOnSprite;
	[SerializeField] private Sprite musicOffSprite;
	[SerializeField] private Button musicBt;
	/* Variables final de partida */
	[SerializeField] private Text scoreMoneyText;
	[SerializeField] private Text scoreTimerText;
	[SerializeField] private Text scoreValueText;
	[SerializeField] private Text addTimeText;
	[SerializeField] private GameObject newRecordText;
	[SerializeField] private GameObject questRewardText;

	/* Scripts con los que se comunica */
	private PlayerController playerScript;
	private Timer timerScript;
	
	/* Variables de temporizador */
	private int timerMinutes;
	private int timerSeconds;

	private int NEOtimerMinutes;
	private int NEOtimerSeconds;

	/* Variables de texto de incremento de tiempo en NEO */
	private float addTimeAlpha;
	[SerializeField] private float alphaProgression;

	/* Variable de  cambio de pantalla*/
	private int currentWindow = 0;


	private void Awake(){
		playerScript = player.GetComponent<PlayerController>();
		timerScript = Camera.main.GetComponent<Timer>();
	}
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

	private void SetScore(){
		scoreMoneyText.text = counterText.text;
		scoreTimerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
		scoreValueText.text = Mathf.FloorToInt(playerScript.GetCurrentScore()).ToString();
	}

	public void UpNewRecordText(){
		newRecordText.SetActive(true);
	}

	public void UpQuestRewardText(float reward){
		questRewardText.GetComponent<Text>().text = "Quest reward:" + reward.ToString("F0");
		questRewardText.SetActive(true);
	}
	public void EnlargeNeoTimeText(float seconds){ /* PARA CLASE TIMER */
		addTimeText.enabled = true;
		addTimeText.text = "+ " + Mathf.Round(seconds).ToString("") + "s";
		addTimeAlpha = 1.0f;
	}

	/* Clicks */
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
	public void ReloadBt(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MenuBt(){
		if(player.GetComponent<PlayerController>().GetDeath() == false)
			timerScript.ChangeState();
		SceneManager.LoadScene(0);
	}

	public void QuitBt(){
		Application.Quit();
	}

	public void SoundBt(){
		if(DataManager.GetSoundOn()){
			soundBt.GetComponent<Image>().sprite = soundOffSprite;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOnSprite;
		}
	}

	public void MusicBt(){
		if(DataManager.GetMusicOn()){
			musicBt.GetComponent<Image>().sprite = musicOffSprite;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOnSprite;
		}
	}
}
