using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManagement : MonoBehaviour {
	[SerializeField] private GameObject gameOverText;
	[SerializeField] private GameObject pauseText;
	[SerializeField] private GameObject pauseBt;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private Text counterText;
	[SerializeField] private Text timerText;
	
	private PlayerController playerScript;
	private PauseController pauseScript;
	private float time;
	private int timerMinutes;
	private int timerSeconds;

	private void Awake(){
		playerScript = player.GetComponent<PlayerController>();
		pauseScript = this.gameObject.GetComponent<PauseController>();
	}
	private void Start () {
		gameOverText.SetActive(false);
		pauseText.SetActive(false);
		pauseMenu.SetActive(false);

		if(PlayerPrefs.GetInt("FPSOn") == 1){
			FPSCounter.SetActive(true);
		}else{
			FPSCounter.SetActive(false);
		}

		time = 0;
	}
	
	private void Update(){

		SetTimer();

		counterText.text = playerScript.GetPickUps().ToString();
		
		if(playerScript.GetDeath()){
			pauseBt.SetActive(false);
			gameOverText.SetActive(true);
			pauseMenu.SetActive(true);
		}else if(pauseScript.GetPaused()){
			pauseText.SetActive(true);
			pauseMenu.SetActive(true);
		}else{
			pauseText.SetActive(false);
			pauseMenu.SetActive(false);
		}
	}

	private void SetTimer(){
		if(playerScript.GetDeath() == false){
			time += Time.deltaTime;
			timerSeconds = (int)Mathf.Floor(time);
			timerMinutes = timerSeconds / 60;

			timerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
		}
	}
}
